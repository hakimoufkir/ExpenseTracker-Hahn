import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponentComponent } from './components/user-component/user-component.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignOutComponent } from './components/sign-out/sign-out.component';
import { authGuard } from './guards/auth.guard';

const routes: Routes = [
  // Public route for sign-in
  { path: 'signin', component: SignInComponent },

  // Protected route for user dashboard
  {
    path: 'user',
    component: UserComponentComponent,
    canActivate: [authGuard], // Guard to protect this route
  },

  // Public route for sign-out (not protected because it ends the session)
  { path: 'signout', component: SignOutComponent },

  // Default route redirecting to sign-in
  { path: '', redirectTo: 'signin', pathMatch: 'full' },

  // Wildcard route for handling unknown routes
  { path: '**', redirectTo: 'signin' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
