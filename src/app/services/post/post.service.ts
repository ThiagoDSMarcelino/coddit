import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { UserResponse } from 'src/app/models/response/user-response';
import { environment } from 'src/environment';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http: HttpClient) { }
  
  getPostByUser = (post: UserResponse, query: string) =>
    this.http.post(`${environment.BACKEND_URL}/post?q=${query}`, post)
}
