import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { MycategoryParams } from '../shared/models/mycategoryParams';
import { IUser1 } from '../shared/models/user1';
import { UsersService } from './users.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  users: IUser1[];
  myParams = new MycategoryParams();
  totalCount: number;
  today = Date.now;

  constructor(private userService: UsersService,
              private toastr: ToastrService,
              private router: Router) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.userService.getUsers(this.myParams)
    .subscribe(response => {
      this.users = response.data;
      this.myParams.pageNumber = response.pageIndex;
      this.myParams.pageSize = response.pageSize;
      this.totalCount = response.count;
    }, error => {
      console.log(error);
    }
    );
  }

  onSearch() {
    this.myParams.query = this.searchTerm.nativeElement.value;
    this.getUsers();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.myParams = new MycategoryParams();
    this.getUsers();
  }

  onPageChanged(event: any) {
    if (this.myParams.pageNumber !== event) {
      this.myParams.pageNumber = event;
      this.getUsers();
    }
}

onLockUser(userId: string) {
  if (confirm('Are you sure you want to unlock this user?')) {
    this.userService.unLockUser(userId)
      .subscribe(
        res => {
          this.getUsers();
          this.toastr.success('User Unlocked!!');
        },
        err => { console.log(err);
         }
      );
  }
}

lockUser(userId: string) {
  if (confirm('Are you sure you want to lock this user?')) {
    this.userService.lockUser(userId)
      .subscribe(
        res => {
          this.getUsers();
          this.toastr.error('User Locked!!');
        },
        err => { console.log(err);
         }
      );
  }
}


}






