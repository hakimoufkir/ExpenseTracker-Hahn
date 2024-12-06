import { MonthEnum } from "./month-enum";

export interface Income {
  id: number;
  description: string;
  amount: number;
  month: MonthEnum;
}
