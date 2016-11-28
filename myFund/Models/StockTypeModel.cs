using Autofac;
using MaterialDesignThemes.Wpf;
using myFund.Domain.Repositories;
using myFund.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFund.Models
{
    public class StockTypeModel : StockBaseModel
    {
        public PackIconKind Kind
        {
            get
            {
                return this.Name == StockTypes.Bond.ToString() ? PackIconKind.ScaleBalance : PackIconKind.TrendingUp;
            }
        }

        public static ObservableCollection<StockTypeModel> GetStockTypes(IStockRepository stockRepository)
        {
            return new ObservableCollection<StockTypeModel>(stockRepository.GetStockTypes().Select(s => new StockTypeModel { Id = s.Id, Name = s.Name }));
        }

        public bool Validate()
        {
            return this.Price > 0 && this.Quantity > 0;
        }

        protected override void NotifyAddCommandChange()
        {
            if (App.Container != null)
                App.Container.Resolve<IStocksViewModel>().NotifyAddCommandChange();
        }
    }
}
