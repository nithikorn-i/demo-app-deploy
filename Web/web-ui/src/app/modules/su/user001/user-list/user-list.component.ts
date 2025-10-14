import { Component } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';
import { PagedResult } from 'src/app/modules/shared/models/paged-result.model';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent {

  public users: User[] = [];
  public total: number = 0;
  public page = 1;
  public pageSize = 10;
  public isSaveMode = false;
  public index!: number;

  constructor(
    private userService: UserService
  ) {
  }

  public ngOnInit(): void {
    this.load();
  }

  public load(page: number = 1): void {
    this.userService.getUsers(page, this.pageSize).subscribe((result: PagedResult<User[]>) => {
      const user = result
      this.users = user.result;
      this.total = user.totalCount;
    });
  }


  public addUser() {
    // route ไปหน้าแก้ไข
    this.users.push({
      id: '',
      fullname: '',
      email: '',
      saveMode: true
    });
    this.index = this.users.length - 1;
  }

  public edit(user: User) {
    // route ไปหน้าแก้ไข
  }

  public delete(id: string) {
    // this.userService.deleteUser(id).subscribe(() => this.load(this.page));
  }

  public save(user: any) {
    this.userService.createUser(user.fullname, user.email).subscribe((result: any) => {
      this.users[this.index].saveMode = !this.users[this.index].saveMode
    });
  }

  public cancel(user: User) {
    console.log(user);
  }
}
