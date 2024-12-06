import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { Images } from '../../../../assets/data/images';
import { AuthServiceService } from '../../../services/auth-service.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  public userOne: string = Images.users.userOne;
  public userName: string = ''; // To store user's name
  public userEmail: string = ''; // To store user's email

  constructor(
    private element: ElementRef,
    private renderer: Renderer2,
    private authService: AuthServiceService
  ) { }

  ngOnInit(): void {
    this.loadUserData();
  }

  onClickProfile = () => {
    const profileDropdownList = this.element.nativeElement.querySelector(
      '.profile-dropdown-list'
    );
    this.renderer.setAttribute(profileDropdownList, 'aria-expanded', 'true');
  };

  private loadUserData(): void {
    this.authService.user().subscribe(
      (claims:any) => {
        const nameClaim = claims.find(
          (claim: any) =>
            claim.type ===
            'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'
        );
        const emailClaim = claims.find(
          (claim: any) =>
            claim.type ===
            'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
        );

        this.userName = nameClaim ? nameClaim.value : 'Guest';
        this.userEmail = emailClaim ? emailClaim.value : 'N/A';
      },
      (error: any) => {
        console.error('Failed to load user data:', error);
      }
    );
  }
}
