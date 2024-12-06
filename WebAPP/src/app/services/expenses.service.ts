import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, switchMap } from 'rxjs';
import { Expense } from '../interfaces/expense';
import { environment } from '../../environments/environment';
import { AuthServiceService } from './auth-service.service';
import { MonthEnum } from '../interfaces/month-enum';
import { CreateExpenseDTO } from '../interfaces/create-expense-dto';
import { CreateIncomeDTO } from '../interfaces/create-income-dto';

@Injectable({
  providedIn: 'root',
})
export class ExpensesService {
  private readonly api = `${environment.apiUrl}/Expense`;

  constructor(private http: HttpClient, private authService: AuthServiceService) { }

  /**
   * Fetch expenses for the authenticated user and a specific month.
   */
  getExpensesForMonth(month: MonthEnum): Observable<Expense[]> {
    return this.authService.getUserId().pipe(
      switchMap((userId) =>
        this.http.get<Expense[]>(`${this.api}/${month}?userId=${userId}`)
      )
    );
  }

  /**
   * Fetch summary of expenses by category for a specific month.
   */
  getExpenseSummaryByCategory(month: MonthEnum): Observable<{ category: string; totalAmount: number }[]> {
    return this.authService.getUserId().pipe(
      switchMap((userId) =>
        this.http.get<{ category: string; totalAmount: number }[]>(
          `${this.api}/${month}/summary-by-category?userId=${userId}`
        )
      )
    );
  }

  /**
   * Add a new expense.
   */
  addExpense(expense: any): Observable<any> {
    return this.http.post(`${this.api}`, expense);
  }

  /**
   * Delete an expense.
   */
  deleteExpense(expenseId: number): Observable<any> {
    return this.http.delete(`${this.api}/${expenseId}`);
  }
}
