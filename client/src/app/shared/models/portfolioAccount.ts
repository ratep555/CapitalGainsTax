export interface IPortfolioAccount {
        id: number;
        stockId: number;
       // userId: string;
        transactionId: number;
        symbol: string;
        totalQuantity: number;
        totalPriceOfPurchase: number;
        totalValueOfCertainStock: number;
        averagePriceOfPurchase: number;
        currentPrice: number;
        email: string;

    }
