export interface ITransactionsForUser {
    id: number;
    stockId: number;
    stock: string;
    userId: string;
    date: string;
    price: number;
    quantity: number;
    purchase: boolean;
    resolved: number;
}
export interface ITransactionsWithProfitAndTraffic {
    listOfTransactions: ITransactionsForUser[];
    totalNetProfit1: number;
    totalTraffic1: number;
}
