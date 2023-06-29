import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ForumData } from 'src/app/DTO/forum-data';

import { environment } from 'src/environment';

@Injectable({
  providedIn: 'root'
})
export class ForumService {

  constructor(private http: HttpClient) { }

  create = (forum: ForumData) =>
    this.http.post(`${environment.BACKEND_URL}/forum/create`, forum)
}