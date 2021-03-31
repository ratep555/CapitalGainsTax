import { IStock } from './stock';

export interface IPagination {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: IStock[];
  }
