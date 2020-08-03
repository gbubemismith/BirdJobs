import { Router } from '@angular/router';
import { TwitterAuthService } from './../services/twitter-auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  photoUrl: string;
  
  constructor(public auth: TwitterAuthService, private router: Router) { 
    this.auth.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl); 
  }

  ngOnInit() {
    
  }

  loggedIn() {
    return this.auth.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.auth.decodedToken = null;
    this.auth.currentUser = null;
    this.router.navigate(['/login']);
  }

}
