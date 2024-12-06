import { ExpenseCategory } from "./expense-category";
import { MonthEnum } from "./month-enum";


export interface Expense {
  id: number;
  description: string;
  amount: number;
  category: ExpenseCategory; // Use the ExpenseCategory enum here
  month: MonthEnum;
}
