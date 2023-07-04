import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserResponse } from 'src/app/DTO/post-data';

import { environment } from 'src/environment';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http: HttpClient) { }
  
  create = (post: UserResponse) =>
    this.http.post(`${environment.BACKEND_URL}/post/create`, post)
}
