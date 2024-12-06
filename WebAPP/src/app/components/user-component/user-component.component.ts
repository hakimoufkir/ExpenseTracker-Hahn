import { Component } from '@angular/core';
import { UserClaim } from '../../interfaces/user-claim';
import { AuthServiceService } from '../../services/auth-service.service';

@Component({
  selector: 'app-user-component',
  templateUrl: './user-component.component.html',
  styleUrl: './user-component.component.css'
})
export class UserComponentComponent {
  userClaims: UserClaim[] = [];
  userName: string = '';
  userEmail: string = '';
  isLoading: boolean = true;

  constructor(private authService: AuthServiceService) { }

  ngOnInit(): void {
    this.fetchUserInfo();
  }

  private fetchUserInfo(): void {
    this.isLoading = true;
    this.authService.user().subscribe(
      (claims: UserClaim[]) => {
        this.userClaims = claims;
        this.userName = claims.find((claim) => claim.type.includes('name'))?.value || 'Unknown User';
        this.userEmail = claims.find((claim) => claim.type.includes('email'))?.value || 'Unknown Email';
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching user information:', error);
        this.isLoading = false;
      }
    );
  }
}
