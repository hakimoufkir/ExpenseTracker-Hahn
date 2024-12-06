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
  selectedMonth: MonthEnum = MonthEnum.January;
  months = Object.entries(MonthEnum); // Get key-value pairs for the dropdown

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
