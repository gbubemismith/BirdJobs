import { RequestToken } from './../models/requestToken';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserDetails } from '../models/userDetails';

@Injectable({
  providedIn: 'root'
})
export class TwitterAuthService {
  baseUrl = environment.apiUrl;
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: UserDetails;
  photoUrl = new BehaviorSubject<string>('../../assets/user.png');
  currentPhotoUrl = this.photoUrl.asObservable();

  constructor(private http: HttpClient) { }

  twitterProfilePicture(photoUrl: string) {
    this.photoUrl.next(photoUrl);
  }

  getRequestToken(): Observable<RequestToken> {
    return this.http.get<RequestToken>(this.baseUrl + 'twitterclient/GetRequestToken');
  }


  getAccessToken(oauth_token, oauth_verifier) {

    let params = new HttpParams();
    params = params.append('oauth_token', oauth_token);
    params = params.append('oauth_verifier', oauth_verifier);

    return this.http.get(this.baseUrl + 'twitterclient/sign-in-with-twitter', {observe: 'response', params})
      .pipe(
        map((response: any) => {
          const user = response.body;
          if (user) {
            localStorage.setItem('token', user.token);
            localStorage.setItem('user', JSON.stringify(user.userDetails));
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
            this.currentUser = user.userDetails;
            this.twitterProfilePicture(this.currentUser.profile_image_url_https);
            
          }

        })
      );

  }

  loggedIn() {
    const token = localStorage.getItem('token');

    return !this.jwtHelper.isTokenExpired(token);
  }
}
