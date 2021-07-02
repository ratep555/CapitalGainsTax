export interface IStock {
    id: number;
    symbol: string;
    currentPrice: number;
    companyName: string;
    country: string;
    category: string;
    totalQuantity: number;
    numberOfEmployees: number;
    sharesOutstanding: number;
    ownShares: number;
    revenue: number;
    expenditure: number;
    enterpriseValue: number;
    dividend: number;
  }

export class INewStock {
    id: number;
    symbol: string;
    currentPrice: number;
    companyName: string;
    country: string;
    category: string;
   // categoryId: number;
  //  countryId: number;
    totalQuantity: number;
  }
