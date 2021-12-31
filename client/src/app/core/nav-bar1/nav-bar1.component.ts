import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { IUser, Role } from 'src/app/shared/models/user';

@Component({
  selector: 'app-nav-bar1',
  templateUrl: './nav-bar1.component.html',
  styleUrls: ['./nav-bar1.component.scss']
})
export class NavBar1Component implements OnInit {
  user: IUser;
  currentUser$: Observable<IUser>;
  isCollapsed = true;

  constructor(private accountService: AccountService) {
    this.accountService.currentUser$.subscribe(x => this.user = x);
  }

  ngOnInit(): void {
    this.currentUser$ = this.accountService.currentUser$;
  }

  get isAdmin() {
    return this.user && this.user.role === Role.Admin;
}

  logout() {
    this.accountService.logout();
  }

}
