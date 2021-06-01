import { IStock } from './stock';

export interface IPagination3 {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: IStock[];
  }
