import { MonthEnum } from "./month-enum";

export interface Budget {
  id: number;
  monthlyLimit: number;
  totalSpent: number;
  month: MonthEnum;
}
