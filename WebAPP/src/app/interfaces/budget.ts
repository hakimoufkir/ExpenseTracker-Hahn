import { MonthEnum } from './month-enum';

export interface Budget {
  id: number;
  monthlyLimit: number;
  month: MonthEnum;
}
