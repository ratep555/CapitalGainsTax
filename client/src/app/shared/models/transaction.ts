export interface IStockTransaction {
    id: number;
    stockId: number;
    userId: string;
    date: Date;
    price: number;
    quantity: number;
    purchase: boolean;
    resolved: number;
    stock: string;
}

export class IStTransaction {
    stockId: number = 0;
    price: number = 0;
    quantity: number = 0;
    purchase: boolean = true;
    resolved: number = 0;
}
