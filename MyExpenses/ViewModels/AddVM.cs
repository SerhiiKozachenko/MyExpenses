using System;
using MyExpenses.Common;
using MyExpenses.Dtos;
using MyExpenses.Enums;
using SQLite;

namespace MyExpenses.ViewModels
{
    public class AddVM
    {
        private TransactionListVM currentList;

        private DataBaseHelper dataBaseHelper = new DataBaseHelper();

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

                    Currency currency = IsUAH ? Currency.UAH : IsUSD ? Currency.USD : Currency.EUR;

                    TransactionType type = this.Income ? TransactionType.Income : TransactionType.Outcome;
                    var transactionDto = new TransactionDto
                    {
                        Id = Guid.NewGuid().ToString(),
                        Amount = amt,
                        CurrencyStr = currency.ToString(),
                        Date = DateTime.Now,
                        Purpose = this.Purpose,
                        TypeStr = type.ToString()
                    };
                    dataBaseHelper.InsertTransaction(transactionDto);

                    this.currentList.AddTransaction(new TransactionVM()
                    {
                        Id = transactionDto.Id,
                        Amount = transactionDto.Amount,
                        Currency = currency.ToString(),
                        Date = transactionDto.Date,
                        Purpose = transactionDto.Purpose,
                        Type = type.ToString()
                    });
                }
                catch (Exception)
                {
                    
                    throw;
                }
               
            });
        }
    }
}