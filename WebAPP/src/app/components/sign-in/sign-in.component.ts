import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthServiceService } from '../../services/auth-service.service';
import { ResponseAPI } from '../../interfaces/response-API';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'] // Corrected the key (styleUrl -> styleUrls)
})
export class SignInComponent implements OnInit {
  loginForm!: FormGroup; // Form for user input
  authFailed: boolean = false; // Tracks authentication failure
  signedIn: boolean = false; // Tracks if the user is signed in

  constructor(
    private authService: AuthServiceService,
    private formBuilder: FormBuilder,
    private router: Router
  ) { }

  ngOnInit(): void {
    // Initialize the sign-in form
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });

    // Check if the user is already signed in
    this.authService.isSignedIn().subscribe((isSignedIn) => {
      this.signedIn = isSignedIn;
    });
  }

  signin(event: Event): void {
    event.preventDefault();
    console.log('Sign-in button clicked'); // Debug log 1

    if (!this.loginForm.valid) {
      console.log('Form is invalid:', this.loginForm.errors); // Debug log 2
      return;
    }

    const email = this.loginForm.get('email')?.value;
    const password = this.loginForm.get('password')?.value;
    console.log('Form values:', { email, password }); // Debug log 3

    this.authService.signIn(email, password).subscribe(
      (response: ResponseAPI) => {
        console.log('API Response:', response); // Debug log 4
        if (response.success) {
          console.log('Sign-in successful, navigating...'); // Debug log 5
          this.router.navigate(['dashboard']);
        }
      },
      (error) => {
        console.error('API Error:', error);
        if (error.status === 401) {
          console.log('Unauthorized: Invalid credentials');
        } else {
          console.log('Unexpected error:', error.message);
        }
        this.authFailed = true;
      }
    );
  }

}
