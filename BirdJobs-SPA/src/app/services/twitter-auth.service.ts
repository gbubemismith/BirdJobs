import { RequestToken } from './../models/requestToken';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TwitterAuthService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getRequestToken(): Observable<RequestToken> {
    return this.http.get<RequestToken>(this.baseUrl + 'twitterclient/GetRequestToken');
  }
}
