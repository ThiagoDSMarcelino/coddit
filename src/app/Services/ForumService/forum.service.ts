import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { UserResponse } from 'src/app/DTO/Response/user-response';
import { ForumData } from 'src/app/DTO/Data/forum-data';
import { environment } from 'src/environment';

@Injectable({
  providedIn: 'root'
})
export class ForumService {

  constructor(private http: HttpClient) { }

  create = (forum: ForumData) =>
    this.http.post(`${environment.BACKEND_URL}/forum/create`, forum)

  get = (forum: UserResponse, query: string) =>
    this.http.post(`${environment.BACKEND_URL}/forum?q=${query}`, forum)
}