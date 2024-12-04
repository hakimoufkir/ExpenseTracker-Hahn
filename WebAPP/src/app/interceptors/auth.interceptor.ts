import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router); // Inject the Router service

  return next(req).pipe(
    tap(
      () => {
        console.log('Request succeeded:', req.url);
      },
      (err: any) => {
        if (err instanceof HttpErrorResponse && err.status === 401) {
          console.log('Unauthorized request:', req.url);
          router.navigate(['signin']);
        }
      }
    )
  );
};
