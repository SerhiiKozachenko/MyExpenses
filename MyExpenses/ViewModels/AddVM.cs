using System;
using MyExpenses.Common;
using MyExpenses.Enums;

namespace MyExpenses.ViewModels
{
    public class AddVM
    {
        private TransactionListVM currentList;

        public Boolean Income { get; set; }

        public String Purpose { get; set; }

        public String Amount { get; set; }

        public Boolean IsUAH { get; set; }

        public Boolean IsUSD { get; set; }

        public Boolean IsEUR { get; set; }

        public RelayCommand Add { get; set; }

        public AddVM(TransactionListVM currentList)
        {
            this.currentList = currentList;

            this.IsUAH = true;

            this.Add = new RelayCommand(() =>
            {
                try
                {
                    Decimal amt = 0;
                    if (!String.IsNullOrEmpty(this.Amount))
                    {
                        var amtStr = this.Amount.Replace('.', ',');
                        amt = Decimal.Parse(amtStr);
                    }
                    
                    this.currentList.Transactions.Add(
                        new TransactionVM()
                        {
                            Amount = amt,
                            Currency = IsUAH ? Currency.UAH.ToString() : IsUSD ? Currency.USD.ToString() : Currency.EUR.ToString(),
                            Date = DateTime.Now,
                            Purpose = Purpose,
                            Type = this.Income ? TransactionType.Income.ToString() : TransactionType.Outcome.ToString()
                        }
                    );
                }
                catch (Exception)
                {
                    
                    throw;
                }
               
            });
        }
    }
}