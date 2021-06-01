export interface IStockToCreate {
    id: number;
    symbol: string;
    currentPrice: number;
    companyName: string;
    countryId: number;
    categoryId: number;
}

export class INewStockToCreate {
    id: number;
    symbol: string;
    currentPrice: number;
    companyName: string;
    countryId: number;
    categoryId: number;
  }
