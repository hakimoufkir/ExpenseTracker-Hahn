import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { UserComponentComponent } from './components/user-component/user-component.component';
import { SignOutComponent } from './components/sign-out/sign-out.component';
import { authInterceptor } from './interceptors/auth.interceptor';
import { provideRouter, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MenubarModule } from 'primeng/menubar';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';


@NgModule({
  declarations: [
    AppComponent,
    SignInComponent,
    UserComponentComponent,
    SignOutComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule, // Ensure this is added for animations
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    MenubarModule, // PrimeNG Menubar module
    InputTextModule,
    ButtonModule,
    CardModule,
  ],
  providers: [
    {
      provide: withInterceptors,
      useValue: [authInterceptor], // Register functional interceptor
      multi: true,
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
