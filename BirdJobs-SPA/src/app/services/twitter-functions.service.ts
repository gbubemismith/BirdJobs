import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { JobsModel } from '../models/jobsModel';

@Injectable({
  providedIn: 'root'
})
export class TwitterFunctionsService {
  baseUrl = environment.apiUrl;
  jwtHelper = new JwtHelperService();

  
  constructor(private http: HttpClient) { }

  loadAllJobs(next?): Observable<JobsModel> {
    return this.http.post<JobsModel>(this.baseUrl + 'twitterclient/SearchTweets', {next : next});
  }
}
