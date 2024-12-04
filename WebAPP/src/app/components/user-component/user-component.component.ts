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
    constructor(private authService: AuthServiceService) {
      this.getUser();
    }
    getUser() {
      this.authService.user().subscribe(
        (result:any) => {
          this.userClaims = result;
        });
    }
}
