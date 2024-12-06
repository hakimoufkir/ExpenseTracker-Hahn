import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthServiceService } from '../../services/auth-service.service';
import { SignUpRequest } from '../../interfaces/sign-up-request';
import { ResponseAPI } from '../../interfaces/response-API';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css'
})
export class SignUpComponent {
  signUpForm!: FormGroup;
  authFailed: boolean = false;
  successMessage: string | null = null;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthServiceService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.signUpForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      name: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  signUp(): void {
    if (!this.signUpForm.valid) {
      return;
    }

    const signUpData: SignUpRequest = this.signUpForm.value;

    this.authService.signUp(signUpData).subscribe(
      (response: ResponseAPI) => {
        if (response.success) {
          this.successMessage = response.message;
          setTimeout(() => this.router.navigate(['signin']), 2000);
        }
      },
      (error) => {
        this.authFailed = true;
        console.error('Error during signup:', error);
      }
    );
  }

  
}
