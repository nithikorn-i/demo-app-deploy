import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent {

  users: User[] = [];
  total: number = 0;
  page = 1;
  pageSize = 10;
  pageRecord = 10;

  constructor(private userService: UserService) {
  }

  ngOnInit(): void {
    this.load(this.page);
  }

  load(page: number = 1): void {
    console.log(page);
    if (page >= this.page) {
      this.page = page;
      this.pageSize = page * this.pageRecord;
      this.userService.getUsers(this.page, this.pageSize).subscribe(result => {
        this.users = result.result;
        this.total = result.totalCount;
        this.page = result.page;
        this.pageSize = result.pageSize;
      });
    }
  }

  edit(user: User) {
    // route ไปหน้าแก้ไข
  }

  delete(id: string) {
    // this.userService.deleteUser(id).subscribe(() => this.load(this.page));
  }
}
