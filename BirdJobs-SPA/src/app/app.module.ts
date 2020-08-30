import { appRoutes } from './routes';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ErrorInterceptorProvider } from './services/error.interceptor';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxSkeletonLoaderModule  } from 'ngx-skeleton-loader';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { HomeComponent } from './home/home.component';
import { TwitterAuthService } from './services/twitter-auth.service';
import { TwitterFunctionsService } from './services/twitter-functions.service';
import { JobsCardComponent } from './jobs-card/jobs-card.component';
import { LoadingComponent } from './loading/loading.component';



export function tokenGetter() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NavBarComponent,
    HomeComponent,
    JobsCardComponent,
    LoadingComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(appRoutes),
    HttpClientModule,
    NgbModule,
    NgxSkeletonLoaderModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ['localhost:5000'],
        disallowedRoutes: ['localhost:5000/api/twitterclient']
      }
    })
    
  ],
  providers: [
    ErrorInterceptorProvider,
    TwitterAuthService,
    TwitterFunctionsService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
