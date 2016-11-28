using myFund.Domain.Repositories;
using myFund.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using enums = myFund.Models.StockTypes;

namespace myFund.ViewModels
{
    public class StocksViewModel : Observable, IStocksViewModel, INotifyPropertyChanged
    {
        private IStockRepository repository;

        private ObservableCollection<StockTypeModel> stockTypes;
        public ObservableCollection<StockTypeModel> StockTypes
        {
            get { return stockTypes; }
            set
            {
                stockTypes = value;
                OnPropertyChanged("StockTypes");
            }
        }

        private ObservableCollection<StockModel> stocks;
        public ObservableCollection<StockModel> Stocks
        {
            get { return stocks; }
            set
            {
                stocks = value;
                OnPropertyChanged("Stocks");
            }
        }

        private SummaryModel bondTotals;
        public SummaryModel BondTotals
        {
            get { return bondTotals; }
            set
            {
                bondTotals = value;
                OnPropertyChanged("BondTotals");
            }
        }

        private SummaryModel equityTotals;
        public SummaryModel EquityTotals
        {
            get { return equityTotals; }
            set
            {
                equityTotals = value;
                OnPropertyChanged("EquityTotals");
            }
        }

        private SummaryModel allTotals;
        public SummaryModel AllTotals
        {
            get { return allTotals; }
            set
            {
                allTotals = value;
                OnPropertyChanged("AllTotals");
            }
        }

        public ICommand AddStockCommand
        {
            get
            {
                return new CommandHandler<StockTypeModel>((item) =>
                {
                    var stockEntity = repository.AddStock(item.Id, item.Price, item.Quantity);
                    if (stockEntity != null)
                    {
                        this.Stocks.Add(StockModel.ToStockModel(stockEntity));
                        this.UpdateTotals();
                    }
                }, (item) => item.Validate());
            }
        }

        public StocksViewModel(IStockRepository stockRepository)
        {
            this.repository = stockRepository;
            this.StockTypes = StockTypeModel.GetStockTypes(stockRepository);
            this.Stocks = StockModel.GetStocks(stockRepository);
            this.UpdateTotals();
        }

        private void UpdateTotals()
        {
            var bondStocks = this.Stocks.Where(s => s.Type.Name == enums.Bond.ToString());
            this.BondTotals = new SummaryModel
            {
                TotalMarketValue = bondStocks.Sum(b => b.MarketValue),
                TotalNumbers = bondStocks.Count()
            };

            var equityStocks = this.Stocks.Where(s => s.Type.Name == enums.Equity.ToString());
            this.EquityTotals = new SummaryModel
            {
                TotalMarketValue = equityStocks.Sum(b => b.MarketValue),
                TotalNumbers = equityStocks.Count()
            };

            var allStocks = this.Stocks;
            this.AllTotals = new SummaryModel
            {
                TotalMarketValue = allStocks.Sum(b => b.MarketValue),
                TotalNumbers = allStocks.Count()
            };

            foreach (var item in this.Stocks)
            {
                item.Weight = item.MarketValue * 100 / this.AllTotals.TotalMarketValue;
            }

            this.BondTotals.TotalWeight = bondStocks.Sum(b => b.Weight);
            this.EquityTotals.TotalWeight = equityStocks.Sum(b => b.Weight);
            this.AllTotals.TotalWeight = allStocks.Sum(b => b.Weight);

            OnPropertyChanged("BondTotals");
            OnPropertyChanged("EquityTotals");
            OnPropertyChanged("AllTotals");
        }

        public void NotifyAddCommandChange()
        {
            OnPropertyChanged("AddStockCommand");
        }
    }
}
