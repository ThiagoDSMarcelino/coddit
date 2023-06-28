import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environment';
import { UserResult } from './user-result';
import { UserData } from './user-data';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  create = (user: UserData) =>
    this.http.post<UserResult>(`${environment.BACKEND_URL}/user/signup`, user)

  login = (user: UserData) =>
    this.http.post<UserResult>(`${environment.BACKEND_URL}/user/signin`, user)
}