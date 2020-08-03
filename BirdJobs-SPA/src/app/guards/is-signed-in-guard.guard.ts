import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import { Observable } from 'rxjs';
import { TwitterAuthService } from '../services/twitter-auth.service';

@Injectable({
  providedIn: 'root'
})
export class IsSignedInGuardGuard implements CanActivate {

  constructor(private auth: TwitterAuthService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean{

    if (this.auth.loggedIn) {
      this.router.navigate(['/home']);
      return true;
    }

    return false;
  }
  
}
