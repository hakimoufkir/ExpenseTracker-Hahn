import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthServiceService } from '../../services/auth-service.service';
import { ResponseAPI } from '../../interfaces/ResponseAPI';

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

    if (!this.loginForm.valid) {
      return;
    }

    const email = this.loginForm.get('email')?.value;
    const password = this.loginForm.get('password')?.value;

    this.authService.signIn(email, password).subscribe(
      (response: ResponseAPI) => {
        if (response.isSuccess) {
          this.router.navigate(['user']);
        }
      },
      (error) => {
        this.authFailed = true;
      }
    );
  }
}
