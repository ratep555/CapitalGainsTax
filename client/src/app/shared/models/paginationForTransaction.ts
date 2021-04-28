import { IStockTransaction } from './transaction';

export interface IPaginationTransaction {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: IStockTransaction[];
  }
