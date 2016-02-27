using System;
using MyExpenses.Common;

namespace MyExpenses.ViewModels
{
    public class AddVM
    {
        private TransactionListVM currentList;

        public Boolean Income { get; set; }

        public String Purpose { get; set; }

        public Decimal? Amount { get; set; }


        public RelayCommand Add { get; set; }

        public AddVM(TransactionListVM currentList)
        {
            this.currentList = currentList;

            this.Add = new RelayCommand(() =>
            {
                
            });
        }
    }
}