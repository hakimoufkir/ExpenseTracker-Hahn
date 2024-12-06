export interface CreateExpenseDTO {
  description: string;
  amount: number;
  category: string;
  month: number; // MonthEnum as number
}
