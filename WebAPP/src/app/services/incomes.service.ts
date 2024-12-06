import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, switchMap } from 'rxjs';
import { Income } from '../interfaces/income';
import { environment } from '../../environments/environment';
import { AuthServiceService } from './auth-service.service';
import { MonthEnum } from '../interfaces/month-enum';
import { CreateIncomeDTO } from '../interfaces/create-income-dto';

@Injectable({
  providedIn: 'root',
})
export class IncomesService {
  private readonly api = `${environment.apiUrl}/Income`;

  constructor(private http: HttpClient, private authService: AuthServiceService) { }

  /**
   * Fetch all incomes for the authenticated user for a specific month.
   * @param month - The target month as `MonthEnum`.
   * @returns An observable of an array of incomes.
   */
  getIncomesForMonth(month: MonthEnum): Observable<Income[]> {
    return this.authService.getUserId().pipe(
      switchMap((userId) =>
        this.http.get<Income[]>(`${this.api}/${month}?userId=${userId}`)
      )
    );
  }

  /**
   * Add a new income for the authenticated user.
   * @param income - The income data to add.
   * @returns An observable for the API response.
   */
  addIncome(income: any): Observable<any> {
    return this.authService.getUserId().pipe(
      switchMap((userId) =>
        this.http.post(`${this.api}?userId=${userId}`, income)
      )
    );
  }

  /**
   * Fetch the total income for a specific month.
   * @param month - The target month as `MonthEnum`.
   * @returns An observable containing the total income as a number.
   */
  getTotalIncomeForMonth(month: MonthEnum): Observable<number> {
    return this.authService.getUserId().pipe(
      switchMap((userId) =>
        this.http.get<number>(`${this.api}/${month}/total?userId=${userId}`)
      )
    );
  }

  /**
   * Delete a specific income for the authenticated user.
   * @param incomeId - The ID of the income to delete.
   * @returns An observable for the API response.
   */
  deleteIncome(incomeId: number): Observable<any> {
    return this.authService.getUserId().pipe(
      switchMap((userId) =>
        this.http.delete(`${this.api}/${incomeId}?userId=${userId}`)
      )
    );
  }
}
