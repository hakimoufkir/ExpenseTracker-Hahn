import { Component } from '@angular/core';
import { DatetimeHelper } from '../_core/helpers/datetime.helper';
import { CommonService } from '../_core/services/common.service';
import { pageTransition } from './animations';
import { PublicRoutes } from './public.routes';
import { Router } from '@angular/router';
import { AppRoutes } from './app.routes';
import { AdminRoutes } from './admin.routes';
import { Images } from '../../../../assets/data/images';
import { AuthServiceService } from '../../../services/auth-service.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
  animations: [pageTransition]
})
export class SignupComponent {
  readonly signupbannerImage:string = Images.auth.signup
  isLoading: boolean = false;
  readonly currentYear: number = DatetimeHelper.currentYear;
  readonly publicRoutes = PublicRoutes;

  constructor(
    public commonService: CommonService,
    private router: Router,
    private authService: AuthServiceService,

  ) { }

  //onFormSubmitHandler = (event: SubmitEvent) => {
  //  event.preventDefault();

  //  this.isLoading = true;

  //  setTimeout(() => {
  //    this.isLoading = false;
  //    this.router.navigate([AppRoutes.Admin, AdminRoutes.Dashboard]);
  //  }, 3000);
  //}


  onFormSubmitHandler = (event: SubmitEvent) => {
    event.preventDefault();

    // Fetch form data
    const email = (document.getElementById('email') as HTMLInputElement)?.value;
    const username = (document.getElementById('username') as HTMLInputElement)?.value;
    const password = (document.getElementById('password') as HTMLInputElement)?.value;
    const confirmPassword = (document.getElementById('confirmpassword') as HTMLInputElement)?.value;

    // Validation
    if (!email || !username || !password || !confirmPassword) {
      console.error('All fields are required.');
      return;
    }
    if (password !== confirmPassword) {
      console.error('Passwords do not match.');
      return;
    }

    // Prepare sign-up data
    const signUpData = { email, name: username, password };

    this.isLoading = true;

    // Call auth service for sign-up
    this.authService.signUp(signUpData).subscribe(
      (response: any) => {
        if (response.success) {
          console.log('Sign-up successful. Redirecting to sign-in page...');
          setTimeout(() => {
            this.router.navigate([this.publicRoutes.Home]);
          }, 2000); // Redirect after success
        } else {
          console.error('Sign-up failed:', response.message);
        }
        this.isLoading = false;
      },
      (error) => {
        console.error('Sign-up error:', error);
        this.isLoading = false;
      }
    );
  };
}
