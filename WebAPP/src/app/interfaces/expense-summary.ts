import { ExpenseCategory } from "./expense-category";

export interface ExpenseSummary {
  category: ExpenseCategory; // Use the enum for consistency
  totalAmount: number;       // Total amount spent in this category
}
