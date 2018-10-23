using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchangeWork
{
    class StockExchange
    {
        private Broker broker;
        private List<Stock> stocks;


        public StockExchange(Broker broker, List<Stock> stocks)
        {
            this.broker = broker;
            this.stocks = stocks;
        }


        public int StartSession() //logowanie i pobór kwoty
        {
            Console.WriteLine("Witamy na giełdzie akcji.");

            bool isTheNameEntered = false;
            string name;

            do
            {
                Console.WriteLine("Proszę podać imię");
                name = Console.ReadLine();

                if (name.Length < 2)
                {
                    Console.WriteLine("Pole imię nie może pozostać puste i musi zawierać więcej niż dwa znaki");
                }
                else
                {
                    broker.Name = name;
                    isTheNameEntered = true;
                }

            } while (isTheNameEntered != true);

            string surname;
            bool isTheSurnameEntered = false;

            do
            {
                Console.WriteLine("Proszę podać nazwisko");

                surname = Console.ReadLine();

                if (surname.Length < 2)
                {
                    Console.WriteLine("Pole nazwisko nie moze pozostać puste i musi zawierać więcej niż dwa znaki");
                }
                else
                {
                    broker.Surname = surname;
                    isTheSurnameEntered = true;
                }

            } while (isTheSurnameEntered != true);

            bool isItNumber = false;
            int amountForPurchaseOfStocks;
            bool isTheAmountEntered = false;

            do
            {
                Console.WriteLine("Proszę podać kwotę przeznaczoną na zakup akcji");

                isItNumber = int.TryParse(Console.ReadLine(), out amountForPurchaseOfStocks);

                if (isItNumber == false)
                {
                    Console.WriteLine("Kwota musi być liczbą");
                }
                else
                {
                    broker.AmountForPurchaseOfStocks = amountForPurchaseOfStocks;
                    isTheAmountEntered = true;
                }

            } while (isTheAmountEntered != true);

            return amountForPurchaseOfStocks;
        }


        public BrokersChoices AcceptanceOfOrdersForStocks()
        {
            bool wasItChosen = false;
            string UserChoice;

            Console.WriteLine("Proszę podać nazwę akcji");
            do
            {
                UserChoice = Console.ReadLine();

                if (UserChoice.Contains(" "))
                {
                    Console.WriteLine("Musisz podać nazwę akcji, aby zostało zrealizowane zamówienie");
                }
                else
                {

                    foreach (Stock stock in stocks)
                    {

                        string nameStock = stock.NameOfStock;

                        if (UserChoice == nameStock)
                        {
                            UserChoice = nameStock;
                            Console.WriteLine("Dziękujemy za wybór");
                            wasItChosen = true;
                            break;
                        }
                    }
                }

                if (wasItChosen == false)
                {
                    Console.WriteLine("Takiej nazwy akcji nie ma w bazie danych");
                    Console.WriteLine("Spróbuj ponownie podać nazwę");
                }

            } while (wasItChosen == false);

            bool isItNumber = false;
            int quantityGivenByUser;
            bool hasBeenQuantityGive = false;

            do
            {
                Console.WriteLine("Podaj interesującą Cię ilość wybranej akcji");

                isItNumber = int.TryParse(Console.ReadLine(), out quantityGivenByUser);

                if(isItNumber == false)
                {
                    Console.WriteLine("Ilość musi być liczbą. Podaj liczbę.");
                }
                else
                {

                    foreach (Stock stock in stocks)
                    {
                        string nameStock = stock.NameOfStock;

                        if (UserChoice == nameStock)
                        {

                            if (quantityGivenByUser < stock.QuantityOfStock)
                            {
                                Console.WriteLine("Dodano " + quantityGivenByUser + " akcji o nazwie " + UserChoice + " do koszyka");
                                Console.WriteLine();
                                hasBeenQuantityGive = true;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Niestety nie mamy tylu akcji :( ");
                                break;
                            }
                        }
                    }
                }
            } while (hasBeenQuantityGive == false);

            BrokersChoices brokersChoices = new BrokersChoices();
            brokersChoices.SelectedNameOfStockByBroker = UserChoice;
            brokersChoices.AnInterestingQuantityOfStocksByBroker = quantityGivenByUser;

            return brokersChoices;
        }

        public void RealizationOfOrders(BrokersChoices selectedNameOfStockByUser, int amountForPurchaseOfStocks)
        {

            string brokersChoice = selectedNameOfStockByUser.SelectedNameOfStockByBroker;
            int priceOfStockSelectedByBroker = 0;

            foreach(Stock stock in stocks)
            {
                string nameStock = stock.NameOfStock;

                if(brokersChoice == nameStock)
                {
                    priceOfStockSelectedByBroker = stock.StockPrice;
                    break;
                }
            }

            int totalAmountForPurchasedStocks = priceOfStockSelectedByBroker * selectedNameOfStockByUser.AnInterestingQuantityOfStocksByBroker;

            Console.WriteLine("Do zapłaty za zakupione akcje jest: " + totalAmountForPurchasedStocks);
            Console.WriteLine();

            int brokersAmountForPurchaseOfStocks = amountForPurchaseOfStocks;

            int remainingPartOfBrokersAmountDesignatedForPurchaseOfStocks = brokersAmountForPurchaseOfStocks - totalAmountForPurchasedStocks;

            selectedNameOfStockByUser.BrokersAmountDesignatedForPurchaseOfStocks = remainingPartOfBrokersAmountDesignatedForPurchaseOfStocks;

            Console.WriteLine("Z przeznaczonej kwoty na zakupy akcji pozostało: " + selectedNameOfStockByUser.BrokersAmountDesignatedForPurchaseOfStocks);
            Console.WriteLine();
        }

        public void EndOfSession()
        {
            Console.WriteLine("Dziękujemy i zapraszamy ponownie.");
        }
    }
}
