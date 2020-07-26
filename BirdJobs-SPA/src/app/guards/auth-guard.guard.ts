import { TwitterAuthService } from './../services/twitter-auth.service';
import { Injectable } from '@angular/core';
import { CanActivate, Router, RouterStateSnapshot} from '@angular/router';


@Injectable({
  providedIn: 'root'
})
export class AuthGuardGuard implements CanActivate {

  constructor(private auth: TwitterAuthService, private router: Router) {
    
    
  }

  canActivate(): boolean {
    if (this.auth.loggedIn())
      return true;
  
    this.router.navigate(['/login']);
    return false;
    
  }
  
}
