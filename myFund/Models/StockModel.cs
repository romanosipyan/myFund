using myFund.Domain.Models;
using myFund.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace myFund.Models
{
    public class StockModel : StockBaseModel
    {
        public StockTypeModel Type { get; set; }

        public decimal MarketValue
        {
            get
            {
                return this.Price * this.Quantity;
            }
        }

        public decimal TransactionCost
        {
            get
            {
                var percent = this.Type.Name == StockTypes.Bond.ToString() ? 2 : 0.5;
                return this.MarketValue * (decimal)percent / 100;
            }
        }

        private decimal weight;
        public decimal Weight
        {
            get
            {
                return this.weight;
            }
            set
            {
                this.weight = value;
                OnPropertyChanged("Weight");
            }
        }

        public static ObservableCollection<StockModel> GetStocks(IStockRepository stockRepository)
        {
            return new ObservableCollection<StockModel>(stockRepository.
                GetStocks().Select(s => ToStockModel(s)));
        }

        public decimal Tolerance
        {
            get
            {
                return this.Type.Name == StockTypes.Bond.ToString() ? 100000 : 200000;
            }
        }

        public bool IsNegative
        {
            get
            {
                return this.MarketValue < 0 || this.TransactionCost > this.Tolerance;
            }
        }

        public static StockModel ToStockModel(IStock entity)
        {
            return new StockModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Quantity = entity.Qunaitity,
                Type = new StockTypeModel
                {
                    Id = entity.StockTypeId,
                    Name = entity.Type.Name
                }
            };
        }
    }
}
