import { MonthEnum } from './month-enum';

export interface CreateBudgetDTO {
  monthlyLimit: number;
  month: MonthEnum;
  userId?: number; // Added for compatibility with backend API
}
