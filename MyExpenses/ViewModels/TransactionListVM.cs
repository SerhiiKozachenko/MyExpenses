using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MyExpenses.Annotations;
using MyExpenses.Enums;

namespace MyExpenses.ViewModels
{
    public class TransactionListVM : INotifyPropertyChanged
    {
        public String Title { get; set; }

        public String SecondaryTitle
        {
            get
            {
                return String.Format("Total +{0}/-{1}",
                    this.Transactions.Where(x => x.Type == TransactionType.Income.ToString())
                        .Sum(x => x.Amount),
                    this.Transactions.Where(x => x.Type == TransactionType.Outcome.ToString())
                        .Sum(x => x.Amount));
                //"Total: +50 230/-23 000";
            }
        }

        public ObservableCollection<TransactionVM> Transactions { get; set; }

        public void AddTransaction(TransactionVM transaction)
        {
            this.Transactions.Add(transaction);
            this.OnPropertyChanged("SecondaryTitle");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}