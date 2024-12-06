import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { DatetimeHelper } from '../_core/helpers/datetime.helper';
import { CommonService } from '../_core/services/common.service';
import { AdminRoutes } from './admin.routes';
import { AppRoutes } from './app.routes';
import { pageTransition } from './animations';
import { Images } from '../../../../assets/data/images';
import { AlertType } from './alert/alert.type';
import { PublicRoutes } from './public.routes';
import { ResponseAPI } from '../../../interfaces/response-API';
import { AuthServiceService } from '../../../services/auth-service.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css'],
  animations: [pageTransition],
})
export class SigninComponent {
  // Static resources
  readonly signinBannerImage: string = Images.bannerLogo;
  readonly publicRoutes = PublicRoutes;
  readonly currentYear: number = DatetimeHelper.currentYear;
  readonly AlertType = AlertType;

  // Component state
  isLoading: boolean = false;
  serverErrors: string[] = [];

  // Reactive form
  signInForm: FormGroup;

  constructor(
    public commonService: CommonService,
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthServiceService
  ) {
    // Initialize the form with validation
    this.signInForm = this.formBuilder.group({
      username: ['', [Validators.required]], // Username is required
      password: ['', [Validators.required]], // Password is required
    });
  }

  /**
   * Handle form submission.
   */
  //protected onFormSubmitHandler(event: SubmitEvent): void {
  //  event.preventDefault();

  //  // Validate the form
  //  if (this.signInForm.invalid) {
  //    this.serverErrors = ['Please fill in all required fields.'];
  //    return;
  //  }

  //  // Simulate a loading state
  //  this.isLoading = true;

  //  setTimeout(() => {
  //    this.isLoading = false;

  //    // Navigate to the dashboard on successful login
  //    this.router.navigate(['/dashboard']); // Redirect to the dashboard

  //  }, 3000);
  //}

  protected onFormSubmitHandler(event: SubmitEvent): void {
    event.preventDefault();

    // Validate the form
    if (this.signInForm.invalid) {
      this.serverErrors = ['Please fill in all required fields.'];
      return;
    }

    // Extract email and password from the form
    const email = this.signInForm.get('username')?.value; // Map username field to email
    const password = this.signInForm.get('password')?.value;

    // Simulate a loading state
    this.isLoading = true;

    // Call the sign-in method
    this.authService.signIn(email, password).subscribe(
      (response: ResponseAPI) => {
        if (response.success) {
          console.log('Sign-in successful, navigating to dashboard...');
          this.router.navigate(['/dashboard']); // Redirect to the dashboard
        } else {
          console.log('Authentication failed.');
          this.serverErrors = [response.message || 'Authentication failed.'];
        }
        this.isLoading = false; // Stop loading spinner
      },
      (error: any) => {
        console.error('API Error:', error);

        // Handle specific error statuses
        if (error.status === 401) {
          this.serverErrors = ['Invalid email or password.'];
        } else {
          this.serverErrors = ['An unexpected error occurred. Please try again later.'];
        }

        this.isLoading = false; // Stop loading spinner
      }
    );
  }
  /**
   * Handle alert close event.
   */
  protected onAlertCloseHandler(event: any): void {
    this.serverErrors = [];
  }
}
