import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserData } from 'src/app/DTO/user-data';

import { environment } from 'src/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  create = (user: UserData) =>
    this.http.post<UserResponse>(`${environment.BACKEND_URL}/user/signup`, user)

  login = (user: UserData) =>
    this.http.post<UserResponse>(`${environment.BACKEND_URL}/user/signin`, user)
}

interface UserResponse {
  token: string
}