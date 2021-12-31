import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, of, ReplaySubject, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { IUser } from '../shared/models/user';
import { IUser1 } from '../shared/models/user1';
import { SocialAuthService } from 'angularx-social-login';
import { GoogleLoginProvider } from 'angularx-social-login';
import { AuthResponseDto } from '../shared/models/authresponsedto';
import { ExternalAuthDto } from '../shared/models/externalauthdto';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<IUser>(JSON.parse(localStorage.getItem('user')));
  private authChangeSub = new Subject<boolean>();
  public authChanged = this.authChangeSub.asObservable();

  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient,
              private router: Router,
              private externalAuthService: SocialAuthService) { }

  public get userValue(): IUser {
    return this.currentUserSource.value;
}

  loadCurrentUser(token: string) {
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get(this.baseUrl + 'account', {headers}).pipe(
     map((user: IUser) => {
       if (user) {
         localStorage.setItem('token', user.token);
         localStorage.setItem('user', JSON.stringify(user));
         this.currentUserSource.next(user);
       }
     })
    );
  }


  login(values: any) {
    return this.http.post(this.baseUrl + 'account/login', values).pipe(
    map((user: IUser) => {
      if (user) {
      localStorage.setItem('token', user.token);
      localStorage.setItem('user', JSON.stringify(user));
      this.currentUserSource.next(user);
      }
    })
    );
}

  register(values: any) {
    return this.http.post(this.baseUrl + 'account/register', values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);

        }
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.externalAuthService.signOut();
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email: string) {
    return this.http.get(this.baseUrl + 'account/emailexists?email=' + email);
  }

  public signInWithGoogle = () => {
    return this.externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }
  public signOutExternal = () => {
    this.externalAuthService.signOut();
  }

  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  }

  /* public externalLogin = (body: ExternalAuthDto) => {
    return this.http.post<IUser>(this.baseUrl + 'account/externallogin', body);
  } */

  externalLogin(values: any) {
    return this.http.post(this.baseUrl + 'account/externallogin', values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);

        }
      })
    );
  }

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  }



}







