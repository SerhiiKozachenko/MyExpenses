using System;
using System.Collections.ObjectModel;

namespace MyExpenses.ViewModels
{
    public class TransactionListVM
    {
        public String Title { get; set; }

        public ObservableCollection<TransactionVM> Transactions { get; set; }  
    }
}