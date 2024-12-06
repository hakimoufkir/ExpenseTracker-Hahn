import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { UserComponentComponent } from './components/user-component/user-component.component';
import { SignOutComponent } from './components/sign-out/sign-out.component';
import { authInterceptor } from './interceptors/auth.interceptor';
import { provideRouter, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MenubarModule } from 'primeng/menubar';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

//========================================================================================================//

import { SigninComponent } from './shared/templates/signin/signin.component';
import { SignupComponent } from './shared/templates/signup/signup.component';
import { PageNotFoundComponent } from './shared/templates/page-not-found/page-not-found.component';
import { DashboardComponent as DashboardComponent2 } from './shared/templates/dashboard/dashboard.component';
import { CommonService } from './shared/templates/_core/services/common.service';
import { AlertComponent } from './shared/templates/signin/alert/alert.component';
import { ValidationErrorComponent } from './shared/templates/signin/validation-error/validation-error.component';
import { SpinnerComponent } from './shared/templates/signin/spinner/spinner.component';
import { HeaderComponent } from './shared/templates/header/header.component';
import { SidebarComponent } from './shared/templates/sidebar/sidebar.component';
import { FooterComponent } from './shared/templates/footer/footer.component';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';
import { AddExpenseComponent } from './components/add-expense/add-expense.component';
import { ExpenseListComponent } from './components/expense-list/expense-list.component';
import { AddBudgetComponent } from './components/add-budget/add-budget.component';
import { AddIncomeComponent } from './components/add-income/add-income.component';
import { IncomeListComponent } from './components/income-list/income-list.component';
import { ToastComponent } from './components/toast/toast.component';


@NgModule({
  declarations: [
    AppComponent,
    SignInComponent,
    UserComponentComponent,
    SignOutComponent,
    SignUpComponent,
    DashboardComponent,
    SigninComponent,
    SignupComponent,
    PageNotFoundComponent,
    DashboardComponent2,
    SidebarComponent,
    HeaderComponent,
    FooterComponent,
    MainLayoutComponent,
    AddExpenseComponent,
    ExpenseListComponent,
    AddBudgetComponent,
    AddIncomeComponent,
    IncomeListComponent,
    ToastComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule, // Ensure this is added for animations
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    MenubarModule, // PrimeNG Menubar module
    InputTextModule,
    ButtonModule,
    CardModule,
    RouterModule, // Required for routing/navigation
    AlertComponent, // Add this
    ValidationErrorComponent, // Add this
    SpinnerComponent // Add this,
  ],
  providers: [
    provideHttpClient(withInterceptors([authInterceptor])), // Register the interceptor here
    CommonService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
