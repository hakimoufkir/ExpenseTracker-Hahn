import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from "../../environments/environment";
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DemoService {
  private readonly api = `${environment.apiUrl}/Demo`;

  constructor(private http: HttpClient) { }

  getData(): Observable<any> {
    return this.http.get(this.api, { responseType: 'text' });
  }
}
