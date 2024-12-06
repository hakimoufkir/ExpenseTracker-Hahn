import { Component } from '@angular/core';
import { ExpensesService } from '../../services/expenses.service';
import { Router } from '@angular/router';
import { ExpenseCategory } from '../../interfaces/expense-category';
import { MonthEnum } from '../../interfaces/month-enum';
import { CreateExpenseDTO } from '../../interfaces/create-expense-dto';


@Component({
  selector: 'app-add-expense',
  templateUrl: './add-expense.component.html',
  styleUrls: ['./add-expense.component.css'],
})
export class AddExpenseComponent {
  newExpense: Partial<CreateExpenseDTO> = {
    category: ExpenseCategory.Food,
    month: MonthEnum.December,
  };

  months = Object.entries(MonthEnum); // Get month enum entries
  categories = Object.values(ExpenseCategory); // Get all categories

  constructor(private expensesService: ExpensesService, private router: Router) { }

  onInputChange(): void {
    console.log('Current Expense Data:', this.newExpense); // Log the current state of the form
  }

  onSubmit(): void {
    const expense: CreateExpenseDTO = {
      description: this.newExpense.description || '',
      amount: this.newExpense.amount || 0,
      category: this.newExpense.category || 'Miscellaneous',
      month: Number(this.newExpense.month) || new Date().getMonth() + 1, // Convert month to a number
    };

    console.log('Submitting expense:', expense); // Debugging info
    this.expensesService.addExpense(expense).subscribe(
      () => {
        this.router.navigate(['/expense-list']);
      },
      (error) => {
        console.error('Failed to add expense:', error);
      }
    );
  }
}
