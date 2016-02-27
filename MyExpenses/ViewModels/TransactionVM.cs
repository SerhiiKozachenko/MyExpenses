using System;
using MyExpenses.Enums;

namespace MyExpenses.ViewModels
{
    public class TransactionVM
    {
        // db fields
        public String Id { get; set; }

        public DateTime Date { get; set; }

        public String Type { get; set; }

        public String Purpose { get; set; }

        public Decimal Amount { get; set; }

        public String Currency { get; set; }

        // computed for bindings
        public String Title
        {
            get
            {
                return string.Format("{0}{1}{2}",
                    this.Type == TransactionType.Income.ToString() ? "+" : "-",
                    this.Amount,
                    this.Currency == Enums.Currency.UAH.ToString() ? "₴" : this.Currency == Enums.Currency.USD.ToString() ? "$" : "€");
            }
        }

        public String Description
        {
            get
            {
                return string.Format("{0}: {1}", Date.ToString("D"), Purpose);
            }
        }
    }
}