import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, switchMap } from 'rxjs';
import { Budget } from '../interfaces/budget';
import { environment } from '../../environments/environment';
import { AuthServiceService } from './auth-service.service';
import { MonthEnum } from '../interfaces/month-enum';
import { CreateBudgetDTO } from '../interfaces/create-budget-dto';

@Injectable({
  providedIn: 'root',
})
export class BudgetsService {
  private readonly api = `${environment.apiUrl}/Budget`;

  constructor(private http: HttpClient, private authService: AuthServiceService) { }

  /**
   * Fetch budget details for the authenticated user and a specific month.
   */
  getBudgetForMonth(month: MonthEnum): Observable<Budget> {
    return this.authService.getUserId().pipe(
      switchMap((userId) => this.http.get<Budget>(`${this.api}/${month}`))
    );
  }

  /**
   * Check if the budget is exceeded for a specific month.
   */
  isBudgetExceeded(month: MonthEnum): Observable<boolean> {
    return this.authService.getUserId().pipe(
      switchMap((userId) => this.http.get<boolean>(`${this.api}/${month}/budget-exceeded`))
    );
  }

  /**
   * Add or update a budget.
   */
  saveBudget(budget: CreateBudgetDTO): Observable<any> {
    return this.authService.getUserId().pipe(
      switchMap((userId) => {
        budget.userId = userId; // Ensure the userId is included in the payload
        return this.http.post(`${this.api}`, budget);
      })
    );
  }
}
