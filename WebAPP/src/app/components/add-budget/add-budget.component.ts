import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { BudgetsService } from '../../services/budgets.service';
import { CreateBudgetDTO } from '../../interfaces/create-budget-dto';

@Component({
  selector: 'app-add-budget',
  templateUrl: './add-budget.component.html',
  styleUrls: ['./add-budget.component.css'],
})
export class AddBudgetComponent {
  newBudget: CreateBudgetDTO = {
    monthlyLimit: 0, // Initialize with default
    month: 1, // Default to January
  };

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

  constructor(private budgetsService: BudgetsService, private router: Router) { }

  onSubmit(): void {
    const budget: CreateBudgetDTO = {
      monthlyLimit: this.newBudget.monthlyLimit || 0,
      month: Number(this.newBudget.month) || new Date().getMonth() + 1
    };

    console.log('Submitting budget:', budget); // Debugging info
    this.budgetsService.saveBudget(budget).subscribe(
      () => {
        this.router.navigate(['/dashboard']);
      },
      (error) => {
        console.error('Failed to add budget:', error);
      }
    );
  }
}
