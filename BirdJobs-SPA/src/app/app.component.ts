import { UserDetails } from './models/userDetails';
import { TwitterAuthService } from './services/twitter-auth.service';
import { Component } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  jwtHelper = new JwtHelperService();

  constructor (private auth: TwitterAuthService) {
    const token = localStorage.getItem('token');
    const user: UserDetails = JSON.parse(localStorage.getItem('user'));

    if (token) {
      auth.decodedToken = this.jwtHelper.decodeToken(token);
    }

    if (user) {
      auth.currentUser = user;
      auth.twitterProfilePicture(user.profile_image_url_https);
    }
    
  }
  
}
