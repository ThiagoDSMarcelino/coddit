import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateForumData } from 'src/app/models/create-forum-data';
import { ForumData } from 'src/app/models/forum-data';
import { ForumPageData } from 'src/app/models/forum-page-data';
import { UserData } from 'src/app/models/user-data';

import { environment } from 'src/environment';

@Injectable({
  providedIn: 'root'
})
export class ForumService {

  constructor(private http: HttpClient) { }

  create = (forum: CreateForumData) =>
    this.http.post<ForumData>(`${environment.BACKEND_URL}/forum/create`, forum)
    
  get = (user: UserData, title: string) =>
    this.http.post<ForumPageData>(`${environment.BACKEND_URL}/forum/${title}`, user)
    
  getAll = (user: UserData, q: string) =>
    this.http.post<ForumData[]>(`${environment.BACKEND_URL}/forum?q=${q}`, user)

  getByUser = (user: UserData) =>
    this.http.post<ForumData[]>(`${environment.BACKEND_URL}/forum/byUser`, user)
}