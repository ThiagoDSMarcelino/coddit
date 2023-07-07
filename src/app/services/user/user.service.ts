import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { CreateUserData } from 'src/app/models/create-user-data';
import { LoginUserData } from 'src/app/models/login-user-data';
import { environment } from 'src/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  create = (user: CreateUserData) =>
    this.http.post<UserResponse>(`${environment.BACKEND_URL}/user/signUp`, user)

  login = (user: LoginUserData) =>
    this.http.post<UserResponse>(`${environment.BACKEND_URL}/user/signIn`, user)
}

interface UserResponse {
  token: string
}