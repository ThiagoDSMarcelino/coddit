import { environment } from 'src/app/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from './user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  create = (user: User) =>
    this.http.post(`${environment.backend_url}/user/signup`, user).subscribe()
}
