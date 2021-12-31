import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { error } from 'selenium-webdriver';
import { AccountService } from '../account.service';
import { SocialUser } from 'angularx-social-login';
import { ExternalAuthDto } from 'src/app/shared/models/externalauthdto';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  public showError: boolean;
  returnUrl: string;
  public errorMessage: string = '';

  constructor(private accountService: AccountService,
              private activatedRoute: ActivatedRoute,
              private toastr: ToastrService,
              private router: Router) { }

  ngOnInit(): void {
    this.returnUrl = this.activatedRoute.snapshot.queryParams.returnUrl || '';
    this.createLoginForm();
  }

  createLoginForm() {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators
      .pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')]),
      password: new FormControl('', Validators.required)
    });
  }

  onSubmit() {
    this.accountService.login(this.loginForm.value).subscribe(() => {
      this.router.navigateByUrl(this.returnUrl);
    },
     error => {
      // this.toastr.error('Authorized, you are not!');
      console.log(error);
    }
    );
  }

  public externalLogin = () => {
    this.showError = false;
    this.accountService.signInWithGoogle()
    .then(res => {
      const user: SocialUser = { ...res };
      console.log(user);
      const externalAuth: ExternalAuthDto = {
        provider: user.provider,
        idToken: user.idToken
      };
      this.validateExternalAuth(externalAuth);
    }, error => console.log(error));
  }

  public externalLogout = () => {
    this.accountService.signOutExternal(); }


  private validateExternalAuth(externalAuth: ExternalAuthDto) {
    this.accountService.externalLogin(externalAuth)
      .subscribe(res => {
       // localStorage.setItem('user', JSON.stringify (res));
        this.router.navigateByUrl('/');
      },
      error => {
        this.errorMessage = error;
        this.showError = true;
        this.accountService.signOutExternal();
      });
  }
}




