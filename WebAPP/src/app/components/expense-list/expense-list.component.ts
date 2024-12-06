import { Component, OnInit } from '@angular/core';
import { ExpensesService } from '../../services/expenses.service';
import { Expense } from '../../interfaces/expense';
import { MonthEnum } from '../../interfaces/month-enum';

@Component({
  selector: 'app-expense-list',
  templateUrl: './expense-list.component.html',
  styleUrls: ['./expense-list.component.css']
})
export class ExpenseListComponent implements OnInit {
  expenses: Expense[] = [];
  selectedMonth: MonthEnum = MonthEnum.December; // Default month

  months = [
    { key: 1, value: 'January' },
    { key: 2, value: 'February' },
    { key: 3, value: 'March' },
    { key: 4, value: 'April' },
    { key: 5, value: 'May' },
    { key: 6, value: 'June' },
    { key: 7, value: 'July' },
    { key: 8, value: 'August' },
    { key: 9, value: 'September' },
    { key: 10, value: 'October' },
    { key: 11, value: 'November' },
    { key: 12, value: 'December' },
  ];


  constructor(private expensesService: ExpensesService) { }

  ngOnInit(): void {
    this.loadExpenses();
  }

  loadExpenses(): void {
    this.expensesService.getExpensesForMonth(this.selectedMonth).subscribe(
      (response) => {
        this.expenses = response;
      },
      (error) => {
        console.error('Error fetching expenses:', error);
      }
    );
  }

  onMonthChange(): void {
    this.loadExpenses();
  }

  deleteExpense(expenseId: number): void {
    this.expensesService.deleteExpense(expenseId).subscribe(
      () => {
        this.expenses = this.expenses.filter((expense) => expense.id !== expenseId);
      },
      (error) => {
        console.error('Error deleting expense:', error);
      }
    );
  }
}
