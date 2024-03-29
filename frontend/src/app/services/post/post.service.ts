import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { CreatePostData } from 'src/app/models/create-post';
import { UserData } from 'src/app/models/user-data';
import { PostData } from 'src/app/models/post-data';
import { environment } from 'src/environment';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http: HttpClient) { }
  
  create = (data: CreatePostData) =>
    this.http.post<PostData>(`${environment.BACKEND_URL}/post/create`, data)

  getByUser = (data: UserData, query: string) =>
    this.http.post<PostData[]>(`${environment.BACKEND_URL}/post?q=${query}`, data)
} 
