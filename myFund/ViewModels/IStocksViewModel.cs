using myFund.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace myFund.ViewModels
{
    public interface IStocksViewModel
    {
        ObservableCollection<StockTypeModel> StockTypes { get; set; }

        ObservableCollection<StockModel> Stocks { get; set; }

        ICommand AddStockCommand { get; }

        void NotifyAddCommandChange();
    }
}
