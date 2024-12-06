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
  selectedMonth: MonthEnum = MonthEnum.December;
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
