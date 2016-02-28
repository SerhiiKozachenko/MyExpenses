using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using MyExpenses.Common;
using MyExpenses.Enums;
using MyExpenses.Views;
using SQLite;

namespace MyExpenses.ViewModels
{
    public class MainVM
    {
        private DataBaseHelper dataBaseHelper = new DataBaseHelper();

        public ObservableCollection<TransactionListVM> Lists { get; set; }

        public TransactionListVM CurrentList { get; set; }

        public RelayCommand Add { get; set; }

        public RelayCommand Details { get; set; }

        public RelayCommand Edit { get; set; }

        public RelayCommand<TransactionVM> Delete { get; set; }

        public MainVM()
        {
            // Pivot items are lists of transactions per each month/year
            // First pivot item is always current month/year
            // If there are some data for the first pivot use it
            // if no then just create empty and save it

            // latest first
            var modelLists = dataBaseHelper.GetTransactions()
                            .GroupBy(x => new {x.Date.Year, x.Date.Month})
                            .OrderByDescending(x => x.Key.Year).ThenByDescending(x => x.Key.Month)
                            .Select(groupByYearAndMonth => new TransactionListVM()
            {
                Title = new DateTime(
                    groupByYearAndMonth.Key.Year,
                    groupByYearAndMonth.Key.Month, 1)
                        .ToString("MMMM yyyy", CultureInfo.InvariantCulture), 
                Transactions = new ObservableCollection<TransactionVM>(
                        groupByYearAndMonth.Select(x => new TransactionVM()
                        {
                            Id = x.Id,
                            Amount = x.Amount,
                            Currency = x.CurrencyStr,
                            Date = x.Date,
                            Purpose = x.Purpose,
                            Type = x.TypeStr
                        })
                    )
            }).ToList();

            // add curent month as start lists if not exists
            if (modelLists.Count > 0)
            {
                var latestTransaction = modelLists.First().Transactions.First();
                if (latestTransaction.Date.Year < DateTime.Now.Year
                    ||
                    (latestTransaction.Date.Year == DateTime.Now.Year &&
                     latestTransaction.Date.Month < DateTime.Now.Month))
                {
                    modelLists.Add(
                        new TransactionListVM()
                        {
                            Title = DateTime.Now.ToString("MMMM yyyy", CultureInfo.InvariantCulture),
                            Transactions = new ObservableCollection<TransactionVM>()
                        }
                    );
                }
            }
            else
            {
                modelLists.Add(
                    new TransactionListVM()
                    {
                        Title = DateTime.Now.ToString("MMMM yyyy", CultureInfo.InvariantCulture),
                        Transactions = new ObservableCollection<TransactionVM>()
                    }
                );
            }

            this.Lists = new ObservableCollection<TransactionListVM>(modelLists);

            /*this.Lists = new ObservableCollection<TransactionListVM>(
                new List<TransactionListVM>
                {
                    // pivot for January 2016
                    new TransactionListVM()
                    {
                        Title = "January 2016",
                        Transactions = new ObservableCollection<TransactionVM>(
                            new List<TransactionVM>
                            {
                                new TransactionVM(){Amount = 300, Currency = "UAH", Date = new DateTime(2016, 01, 15), Id = Guid.NewGuid().ToString(), Purpose = "L35 Salary", Type = TransactionType.Income.ToString()},
                                new TransactionVM(){Amount = 100, Currency = "UAH", Date = new DateTime(2016, 01, 29), Id = Guid.NewGuid().ToString(), Purpose = "Home internet", Type = TransactionType.Outcome.ToString()},
                            }
                        )
                    },

                    // pivot for February 2016
                    new TransactionListVM()
                    {
                        Title = "Februrary 2016",
                        Transactions = new ObservableCollection<TransactionVM>(
                            new List<TransactionVM>
                            {
                                new TransactionVM(){Amount = 700, Currency = "UAH", Date = new DateTime(2016, 02, 10), Id = Guid.NewGuid().ToString(), Purpose = "L35 Salary", Type = TransactionType.Income.ToString()},
                                new TransactionVM(){Amount = 200, Currency = "UAH", Date = new DateTime(2016, 02, 20), Id = Guid.NewGuid().ToString(), Purpose = "Home internet", Type = TransactionType.Outcome.ToString()},
                            }
                        )
                    }
                }
            );*/

            // actions
            this.Add = new RelayCommand(async () =>
            {
                var dlg = new AddDialog();
                var addVm = new AddVM(this.CurrentList);
                dlg.DataContext = addVm;
                await dlg.ShowAsync();
            });

            this.Details = new RelayCommand(() =>
            {
                
            });

            this.Edit = new RelayCommand(() =>
            {

            });

            this.Delete = new RelayCommand<TransactionVM>((transaction) =>
            {
                this.dataBaseHelper.DeleteTransaction(transaction.Id);
                this.CurrentList.RemoveTransaction(transaction);
            });
        }
    }
}