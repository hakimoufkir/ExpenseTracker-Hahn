import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router); // Inject the Router service

  // Clone the request to add withCredentials
  const modifiedRequest = req.clone({
    withCredentials: true, // Ensure cookies are sent with the request
  });

  return next(modifiedRequest).pipe(
    tap(
      () => {
        console.log('int : Request succeeded:', req.url);
      },
      (err: any) => {
        if (err instanceof HttpErrorResponse && err.status === 401) {
          console.log('int : Unauthorized request:', req.url);
          router.navigate(['']); // Redirect to the sign-in page
        }
      }
    )
  );
};
