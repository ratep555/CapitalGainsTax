import { ICategory } from './category';

export interface IPagination1 {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: ICategory[];
  }
