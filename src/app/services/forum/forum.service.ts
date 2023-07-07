import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ForumResponse } from 'src/app/models/response/forum-response';
import { UserResponse } from 'src/app/models/response/user-response';
import { ForumData } from 'src/app/models/data/forum-data';
import { environment } from 'src/environment';

@Injectable({
  providedIn: 'root'
})
export class ForumService {

  constructor(private http: HttpClient) { }

  get = (user: UserResponse, title: string) =>
    this.http.post<ForumResponse>(`${environment.BACKEND_URL}/forum/${title}`, user)

  create = (forum: ForumData) =>
    this.http.post(`${environment.BACKEND_URL}/forum/create`, forum)

  getUserForums = (user: UserResponse) =>
    this.http.post<ForumResponse[]>(`${environment.BACKEND_URL}/forum/userforums`, user)

  getNewForums = (user: UserResponse) =>
    this.http.post<ForumResponse[]>(`${environment.BACKEND_URL}/forum/newforums`, user)
}