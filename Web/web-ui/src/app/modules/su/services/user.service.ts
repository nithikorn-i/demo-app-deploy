import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { PagedResult } from '../../shared/models/paged-result.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private currentPort = window.location.port;

  // private api = 'http://localhost:5062/api/Win001';
  // private api = `http://localhost:${this.currentPort}/api/Win001`
  private api = `http://demo-app-svc.dev.svc.cluster.local:8000/api/Win001`

  constructor(private http: HttpClient) { }

  public getUsers(page = 1, pageSize = 10): Observable<PagedResult<User[]>> {
    return this.http.get<PagedResult<User[]>>(
      `${this.api}/getAllUser?Page=${page}&PageSize=${pageSize}`
    );
  }

  public createUser(fullname: string, email: string): Observable<any> {
    const body = {
      Fullname: fullname,
      Email: email
    };

    return this.http.post<any>(
      `${this.api}/CreateUser`,
      body
    );
  }

}
