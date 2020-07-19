import { RequestToken } from './../models/requestToken';
import { TwitterAuthService } from './../services/twitter-auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  private authWindow: Window;
  private requestToken: RequestToken;

  constructor(private twitterService: TwitterAuthService) { }

  ngOnInit() {
    this.twitterService.getRequestToken().subscribe(response => this.requestToken = response);
  }

  launchTwitterLogin() {
    console.log('Token',this.requestToken.oauth_token);
    this.authWindow = window.open("https://api.twitter.com/oauth/authenticate?" + this.requestToken.oauth_token);
  }

}
