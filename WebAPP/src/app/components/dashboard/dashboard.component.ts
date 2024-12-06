import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { ExpensesService } from '../../services/expenses.service';
import { IncomesService } from '../../services/incomes.service';
import { BudgetsService } from '../../services/budgets.service';
import { ToastService } from '../../services/toast.service';
import { Chart, registerables } from 'chart.js';
import { MonthEnum } from '../../interfaces/month-enum';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit, AfterViewInit {
  @ViewChild('expensePieChart') expensePieChartRef!: ElementRef;
  @ViewChild('incomeExpenseChart') incomeExpenseChartRef!: ElementRef;

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
  incomeExpenseChart: Chart | null = null;

  constructor(
    private budgetsService: BudgetsService,
    private expensesService: ExpensesService,
    private incomesService: IncomesService,
    public toastService: ToastService
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
    Promise.all([
      this.loadBudgetPromise(),
      this.loadExpensesPromise(),
      this.loadIncomesPromise(),
    ]).then(() => {
      this.calculateRemainingBudget();
      this.checkBudgetExceeded();
      this.loadCharts();
    });
  }

  loadBudgetPromise(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.budgetsService.getBudgetForMonth(this.currentMonth).subscribe(
        (budget) => {
          this.monthlyBudget = budget.monthlyLimit;
          resolve();
        },
        (error) => {
          console.error('Error loading budget:', error);
          reject(error);
        }
      );
    });
  }

  loadExpensesPromise(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.expensesService.getExpensesForMonth(this.currentMonth).subscribe(
        (expenses) => {
          this.totalExpenses = expenses.reduce((sum, expense) => sum + expense.amount, 0);
          resolve();
        },
        (error) => {
          console.error('Error loading expenses:', error);
          reject(error);
        }
      );
    });
  }

  loadIncomesPromise(): Promise<void> {
    return new Promise((resolve, reject) => {
      this.incomesService.getTotalIncomeForMonth(this.currentMonth).subscribe(
        (totalIncome: any) => {
          this.totalIncomes = totalIncome.totalIncome;
          resolve();
        },
        (error) => {
          console.error('Error loading incomes:', error);
          reject(error);
        }
      );
    });
  }

  calculateRemainingBudget(): void {
    this.remainingBudget = this.monthlyBudget - this.totalExpenses;
  }

  checkBudgetExceeded(): void {
    this.isBudgetExceeded = this.remainingBudget < 0;
    if (this.isBudgetExceeded) {
      this.toastService.addToast(
        'Warning: Your expenses have exceeded your monthly budget!',
        'warning'
      );
    }
  }

  loadCharts(): void {
    this.loadExpenseChart();
    this.loadIncomeExpenseChart();
  }

  loadExpenseChart(): void {
    const chartId = this.expensePieChartRef.nativeElement.id;
    if (Chart.getChart(chartId)) {
      Chart.getChart(chartId)?.destroy();
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

  loadIncomeExpenseChart(): void {
    const chartId = this.incomeExpenseChartRef.nativeElement.id;
    if (Chart.getChart(chartId)) {
      Chart.getChart(chartId)?.destroy();
    }

    const labels = ['Income', 'Expenses'];
    const data = [this.totalIncomes, this.totalExpenses];

    this.incomeExpenseChart = new Chart(this.incomeExpenseChartRef.nativeElement, {
      type: 'bar',
      data: {
        labels: labels,
        datasets: [
          {
            label: 'Amount',
            data: data,
            backgroundColor: ['#4CAF50', '#FF6384'],
          },
        ],
      },
      options: {
        responsive: true,
        plugins: {
          legend: {
            display: false,
          },
        },
        scales: {
          x: { title: { display: true, text: 'Type' } },
          y: { title: { display: true, text: 'Amount' } },
        },
      },
    });
  }
}
