import { CanActivateFn, Router } from '@angular/router';
import { AuthServiceService } from '../services/auth-service.service';
import { inject } from '@angular/core';
import { map } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthServiceService); // Inject AuthService
  const router = inject(Router); // Inject Router

  // Check if the user is signed in
  return authService.isSignedIn().pipe(
    map((isSignedIn) => {
      if (!isSignedIn) {
        router.navigate(['signin']); // Redirect to signin if not authenticated
        return false;
      }
      return true; // Allow access if authenticated
    })
  );
};
