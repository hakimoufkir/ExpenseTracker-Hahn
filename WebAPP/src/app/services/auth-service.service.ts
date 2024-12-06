import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, catchError, of } from 'rxjs';
import { UserClaim } from '../interfaces/user-claim';
import { SignUpRequest } from '../interfaces/sign-up-request';
import { ResponseAPI } from '../interfaces/response-API';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthServiceService {
  private readonly api = `${environment.apiUrl}/auth`;

  constructor(private http: HttpClient) { }

  public signUp(data: SignUpRequest): Observable<ResponseAPI> {
    return this.http.post<ResponseAPI>(`${this.api}/signup`, data);
  }

  public signIn(email: string, password: string): Observable<ResponseAPI> {
    return this.http.post<ResponseAPI>(
      `${this.api}/signin`,
      { email, password },
      { withCredentials: true }
    );
  }

  public signOut(): Observable<any> {
    return this.http.get(`${this.api}/signout`, { withCredentials: true });
  }

  public user(): Observable<UserClaim[]> {
    return this.http.get<UserClaim[]>(`${this.api}/user`, { withCredentials: true });
  }

  public isSignedIn(): Observable<boolean> {
    return this.user().pipe(
      map((userClaims) => userClaims.length > 0),
      catchError(() => of(false))
    );
  }

  /**
   * Get the authenticated user's ID.
   */
  public getUserId(): Observable<number> {
    return this.user().pipe(
      map((claims) => {
        const userIdClaim = claims.find((claim) => claim.type === 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier');
        if (!userIdClaim) {
          throw new Error('UserId claim not found');
        }
        return +userIdClaim.value;
      }),
      catchError(() => {
        console.error('Failed to retrieve userId.');
        return of(-1); // Return a default value or handle it as necessary
      })
    );
  }
}
