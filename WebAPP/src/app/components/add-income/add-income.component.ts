import { Component } from '@angular/core';
import { IncomesService } from '../../services/incomes.service';
import { Router } from '@angular/router';
import { MonthEnum } from '../../interfaces/month-enum';
import { CreateIncomeDTO } from '../../interfaces/create-income-dto';

@Component({
  selector: 'app-add-income',
  templateUrl: './add-income.component.html',
  styleUrls: ['./add-income.component.css'],
})
export class AddIncomeComponent {
  newIncome: Partial<CreateIncomeDTO> = {
    month: MonthEnum.January,
  };

  months = Object.entries(MonthEnum); // Get month enum entries

  constructor(private incomesService: IncomesService, private router: Router) { }

  onInputChange(): void {
    console.log('Current Income Data:', this.newIncome); // Log the current state of the form
  }

  onSubmit(): void {
    const income: CreateIncomeDTO = {
      description: this.newIncome.description || '',
      amount: this.newIncome.amount || 0,
      month: Number(this.newIncome.month) || new Date().getMonth() + 1, // Convert month to a number
    };

    console.log('Submitting income:', income); // Debugging info
    this.incomesService.addIncome(income).subscribe(
      () => {
        this.router.navigate(['/income-list']);
      },
      (error) => {
        console.error('Failed to add income:', error);
      }
    );
  }
}
