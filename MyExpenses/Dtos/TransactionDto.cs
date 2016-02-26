using System;
using MyExpenses.Enums;

namespace MyExpenses.Dtos
{
    public class TransactionDto : BaseDto
    {
        public String TypeStr { get; set; }

        [SQLite.Ignore]
        public TransactionType Type
        {
            get
            {
                switch (TypeStr)
                {
                    case "Income": 
                        return TransactionType.Income;

                    case "Outcome":
                        return TransactionType.Outcome;
                }

                throw new InvalidCastException("TransactionType: " + TypeStr + " is undefined");
            }
        }

        public String Purpose { get; set; }

        public Decimal Amount { get; set; }

        public String CurrencyStr { get; set; }

        [SQLite.Ignore]
        public Currency Currency
        {
            get
            {
                switch (CurrencyStr)
                {
                    case "USD":
                        return Currency.USD;

                    case "EUR":
                        return Currency.EUR;

                    case "UAH":
                        return Currency.UAH;
                }

                throw new InvalidCastException("Currency: " + CurrencyStr + " is undefined");
            }
        }
    }
}