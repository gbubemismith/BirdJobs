import { TwitterAuthService } from './../services/twitter-auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(public auth: TwitterAuthService) { }

  ngOnInit() {
  }

  loggedIn() {
    return this.auth.loggedIn();
  }

}
