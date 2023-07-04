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

  create = (forum: ForumData) =>
    this.http.post(`${environment.BACKEND_URL}/forum/create`, forum)

  get = (user: UserResponse, query: string) =>
    this.http.post<ForumResponse[]>(`${environment.BACKEND_URL}/forum?q=${query}`, user)

  getAll = (user: UserResponse) =>
    this.http.post<ForumResponse[]>(`${environment.BACKEND_URL}/forum/userForums`, user)
}