import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { Routes } from '@angular/router'
import { AuthGuardGuard } from './guards/auth-guard.guard';
import { IsSignedInGuardGuard } from './guards/is-signed-in-guard.guard';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuardGuard]},
    { path: 'login', component: LoginComponent},
    { path: 'home', component: HomeComponent, canActivate: [AuthGuardGuard]},
    { path: '**', redirectTo: '', pathMatch: 'full'}
]