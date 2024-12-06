import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
//import { UserComponentComponent } from './components/user-component/user-component.component';
//import { SignInComponent } from './components/sign-in/sign-in.component';
//import { SignOutComponent } from './components/sign-out/sign-out.component';
import { authGuard } from './guards/auth.guard';
//import { SignUpComponent } from './components/sign-up/sign-up.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { PageNotFoundComponent } from './shared/templates/page-not-found/page-not-found.component';
import { SigninComponent } from './shared/templates/signin/signin.component';
import { SignupComponent } from './shared/templates/signup/signup.component';
import { ExpenseListComponent } from './components/expense-list/expense-list.component';
import { AddExpenseComponent } from './components/add-expense/add-expense.component';
import { AddIncomeComponent } from './components/add-income/add-income.component';
import { AddBudgetComponent } from './components/add-budget/add-budget.component';
import { IncomeListComponent } from './components/income-list/income-list.component';

//const routes: Routes = [
//  { path: 'signup', component: SignUpComponent },
//  { path: 'signout', component: SignOutComponent },
//  { path: 'signin', component: SignInComponent },
//  { path: 'user', component: UserComponentComponent, canActivate: [authGuard] },
//  { path: 'dashboard', component: DashboardComponent, canActivate: [authGuard] },
//  { path: '', redirectTo: 'signin', pathMatch: 'full' },
//  { path: '**', redirectTo: 'signin' },
//];

const routes: Routes = [
  { path: '', component: SigninComponent },
  { path: 'signup', component: SignupComponent },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent, canActivate: [authGuard] },
      { path: 'add-expense', component: AddExpenseComponent, canActivate: [authGuard] }, // Add Expense route
      { path: 'expense-list', component: ExpenseListComponent, canActivate: [authGuard] }, // Expense List route
      { path: 'add-income', component: AddIncomeComponent, canActivate: [authGuard] }, // Add Income route
      { path: 'income-list', component: IncomeListComponent, canActivate: [authGuard] }, // Income List route
      { path: 'add-budget', component: AddBudgetComponent, canActivate: [authGuard] }, // Add Budget route
    ],
  },
  { path: '**', component: PageNotFoundComponent }, // Wildcard for 404
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
