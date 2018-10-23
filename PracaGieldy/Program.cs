using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeWork
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Stock> stock = new List<Stock>();

            stock.Add(new Stock() { NameOfStock = "A", QuantityOfStock = 10, StockPrice = 50 });
            stock.Add(new Stock() { NameOfStock = "B", QuantityOfStock = 5, StockPrice = 100 });
            stock.Add(new Stock() { NameOfStock = "C", QuantityOfStock = 15, StockPrice = 20 });
            stock.Add(new Stock() { NameOfStock = "D", QuantityOfStock = 20, StockPrice = 250 });
            stock.Add(new Stock() { NameOfStock = "E", QuantityOfStock = 2, StockPrice = 80 });
            
            Broker broker = new Broker();
            StockExchange stockExchange = new StockExchange(broker, stock);

            int amountForPurchaseOfStocks = stockExchange.StartSession();
            BrokersChoices selectedNameOfStockByUser = stockExchange.AcceptanceOfOrdersForStocks();
            stockExchange.RealizationOfOrders(selectedNameOfStockByUser, amountForPurchaseOfStocks);
            stockExchange.EndOfSession();

            Console.Read();
        }
    }
}
