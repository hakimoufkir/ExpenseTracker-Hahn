import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, catchError, of } from 'rxjs';
import { UserClaim } from '../interfaces/user-claim';
import { ResponseAPI } from '../interfaces/ResponseAPI';

@Injectable({
  providedIn: 'root',
})
export class AuthServiceService {
  private api = 'https://localhost:8081/api/auth';

  constructor(private http: HttpClient) { }

  public signIn(email: string, password: string): Observable<ResponseAPI> {
    return this.http.post<ResponseAPI>(`${this.api}/signin`, { email, password }, { withCredentials: true });
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
}
