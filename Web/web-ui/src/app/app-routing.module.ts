import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
// import { UserListComponent } from './modules/su/user001/user-list/user-list.component';
import { ApiJobComponent } from './modules/MockLogApi/api-job/api-job.component';


const routes: Routes = [{ path: '', component: ApiJobComponent }] // เส้นทาง root (หน้าแรก)];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
