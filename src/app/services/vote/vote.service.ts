import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { VoteData } from 'src/app/models/vote-data';
import { environment } from 'src/environment';

@Injectable({
  providedIn: 'root'
})
export class VoteService {

  constructor(private http: HttpClient) { }

  create = (data: VoteData) =>
    this.http.post(`${environment.BACKEND_URL}/vote/create`, data)

  update = (data: VoteData) =>
    this.http.post(`${environment.BACKEND_URL}/vote/update`, data)

  delete = (data: VoteData) =>
    this.http.post(`${environment.BACKEND_URL}/vote/delete`, data)
}
