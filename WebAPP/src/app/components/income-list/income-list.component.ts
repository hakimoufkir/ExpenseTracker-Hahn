import { Component, OnInit } from '@angular/core';
import { IncomesService } from '../../services/incomes.service';
import { Income } from '../../interfaces/income';
import { MonthEnum } from '../../interfaces/month-enum';

@Component({
  selector: 'app-income-list',
  templateUrl: './income-list.component.html',
  styleUrls: ['./income-list.component.css']
})
export class IncomeListComponent implements OnInit {
  incomes: Income[] = [];
  selectedMonth: MonthEnum = MonthEnum.January;
  months = Object.entries(MonthEnum); // Get key-value pairs for the dropdown

  constructor(private incomesService: IncomesService) { }

  ngOnInit(): void {
    this.loadIncomes();
  }

  loadIncomes(): void {
    this.incomesService.getIncomesForMonth(this.selectedMonth).subscribe(
      (response) => {
        this.incomes = response;
      },
      (error) => {
        console.error('Error fetching incomes:', error);
      }
    );
  }

  onMonthChange(): void {
    this.loadIncomes();
  }
}
