import { Component } from '@angular/core';
import { AuthServiceService } from '../../services/auth-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-out',
  templateUrl: './sign-out.component.html',
  styleUrl: './sign-out.component.css'
})
export class SignOutComponent {
  constructor(private authService: AuthServiceService, private router: Router) {
    this.signout();
  }
  private signout(): void {
    this.authService.signOut().subscribe(
      () => {
        this.clearSession();
        console.log('Signed out successfully');
        this.router.navigate(['/signin']);
      },
      (error) => {
        console.error('Sign-out failed:', error);
      }
    );
  }

  private clearSession(): void {
    document.cookie = '.AspNetCore.Cookies=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
    localStorage.clear();
    sessionStorage.clear();
  }
}
