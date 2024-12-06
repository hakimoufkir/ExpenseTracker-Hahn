import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { ExpensesService } from '../../services/expenses.service';
import { IncomesService } from '../../services/incomes.service';
import { BudgetsService } from '../../services/budgets.service';
import { Chart, registerables } from 'chart.js';
import { MonthEnum } from '../../interfaces/month-enum';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit, AfterViewInit {
  @ViewChild('expensePieChart') expensePieChartRef!: ElementRef;
  @ViewChild('incomeBarChart') incomeBarChartRef!: ElementRef;
  @ViewChild('budgetProgressBar') budgetProgressBarRef!: ElementRef;

  months = [
    { label: 'January', value: MonthEnum.January },
    { label: 'February', value: MonthEnum.February },
    { label: 'March', value: MonthEnum.March },
    { label: 'April', value: MonthEnum.April },
    { label: 'May', value: MonthEnum.May },
    { label: 'June', value: MonthEnum.June },
    { label: 'July', value: MonthEnum.July },
    { label: 'August', value: MonthEnum.August },
    { label: 'September', value: MonthEnum.September },
    { label: 'October', value: MonthEnum.October },
    { label: 'November', value: MonthEnum.November },
    { label: 'December', value: MonthEnum.December },
  ];
  currentMonth: MonthEnum = MonthEnum.December;

  monthlyBudget: number = 0;
  remainingBudget: number = 0;
  isBudgetExceeded: boolean = false;
  totalExpenses: number = 0;
  totalIncomes: number = 0;

  expenseChart: Chart | null = null;
  incomeChart: Chart | null = null;

  constructor(
    private budgetsService: BudgetsService,
    private expensesService: ExpensesService,
    private incomesService: IncomesService
  ) {
    Chart.register(...registerables);
  }

  ngOnInit(): void {
    this.loadDashboardData();
  }

  ngAfterViewInit(): void {
    this.loadCharts();
  }

  onMonthChange(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.loadBudget();
    this.loadExpenses();
    this.loadIncomes();
    this.loadCharts();
  }

  loadBudget(): void {
    this.budgetsService.getBudgetForMonth(this.currentMonth).subscribe(
      (budget) => {
        this.monthlyBudget = budget.monthlyLimit;
        this.remainingBudget = budget.monthlyLimit - budget.totalSpent;
        this.isBudgetExceeded = this.remainingBudget < 0;
      },
      (error) => {
        console.error('Error loading budget:', error);
      }
    );
  }

  loadExpenses(): void {
    this.expensesService.getExpensesForMonth(this.currentMonth).subscribe(
      (expenses) => {
        this.totalExpenses = expenses.reduce((sum, expense) => sum + expense.amount, 0);
        this.loadExpenseChart();
      },
      (error) => {
        console.error('Error loading expenses:', error);
      }
    );
  }

  loadIncomes(): void {
    this.incomesService.getTotalIncomeForMonth(this.currentMonth).subscribe(
      (totalIncome: any) => {
        this.totalIncomes = totalIncome.totalIncome;
        this.loadIncomeChart();
      },
      (error) => {
        console.error('Error loading incomes:', error);
      }
    );
  }

  loadCharts(): void {
    this.loadExpenseChart();
    this.loadIncomeChart();
  }

  loadExpenseChart(): void {
    if (this.expenseChart) {
      this.expenseChart.destroy();
    }

    this.expensesService.getExpenseSummaryByCategory(this.currentMonth).subscribe(
      (summary) => {
        const categories = summary.map((item) => item.category);
        const amounts = summary.map((item) => item.totalAmount);

        this.expenseChart = new Chart(this.expensePieChartRef.nativeElement, {
          type: 'pie',
          data: {
            labels: categories,
            datasets: [
              {
                data: amounts,
                backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4CAF50', '#FF5722'],
              },
            ],
          },
          options: { responsive: true, plugins: { legend: { position: 'top' } } },
        });
      },
      (error) => {
        console.error('Error loading expense chart data:', error);
      }
    );
  }

  loadIncomeChart(): void {
    if (this.incomeChart) {
      this.incomeChart.destroy();
    }

    this.incomeChart = new Chart(this.incomeBarChartRef.nativeElement, {
      type: 'bar',
      data: {
        labels: ['Total Income'],
        datasets: [
          {
            label: 'Income',
            data: [this.totalIncomes],
            backgroundColor: '#36A2EB',
          },
        ],
      },
      options: { responsive: true, plugins: { legend: { display: false } } },
    });
  }
}
