import { ICountry } from './country';

export interface IPagination2 {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: ICountry[];
  }
