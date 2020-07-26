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

    if (token) {
      auth.decodedToken = this.jwtHelper.decodeToken(token);
    }
  }
  
}
