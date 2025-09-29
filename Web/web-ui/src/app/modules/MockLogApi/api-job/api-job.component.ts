import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { interval, Subscription } from 'rxjs';
import { UserService } from '../../su/services/user.service';

@Component({
  selector: 'app-api-job',
  templateUrl: './api-job.component.html'
})
export class ApiJobComponent {
  intervalMinutes = 5 * 60; // default 5 นาที
  isRunning = false;
  jobSubscription: Subscription | null = null;
  resultValue: any[] = ['no result'];
  i = 0;

  constructor( private userService: UserService
  ) {}

  startJob() {
    if (this.isRunning) return;
    this.isRunning = true;

    const intervalMs = this.intervalMinutes * 1000;

    this.jobSubscription = interval(intervalMs).subscribe(() => {
      this.callApi();
    });

    // Call once immediately
    this.callApi();
  }

  stopJob() {
    if (this.jobSubscription) {
      this.jobSubscription.unsubscribe();
      this.jobSubscription = null;
    }
    this.i = 0;
    this.isRunning = false;
  }

  callApi() {
    this.i++;
    this.userService.getUsers().forEach((data) => {
      this.resultValue = data.result;
    });
  }
}
