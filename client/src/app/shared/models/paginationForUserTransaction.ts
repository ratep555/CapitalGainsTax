import { ITransactionsForUser } from './transacionsForUser';

export interface IPaginationTransaction {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: ITransactionsForUser[];
  }
