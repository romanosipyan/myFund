using myFund.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myFund.Domain.Models;
using myFund.Data.Context;
using myFund.Data.Entities;

namespace myFund.Data.Repositories
{
    public class StockRepository : IStockRepository
    {
        public IEnumerable<IStock> GetStocks()
        {
            using (var db = new MyFundContext())
            {
                var query = db.Stocks.Include("StockType").Select(x => x).OrderBy(x => x.Name);

                return query.ToList();
            }
        }

        public IStock AddStock(int stockTypeId, decimal price, int quanitity)
        {
            using (var db = new MyFundContext())
            {
                var stockCount = (from b in db.Stocks
                                  where b.StockTypeId == stockTypeId
                                  select b).Count();

                var stockType = (from b in db.StockTypes
                                 where b.Id == stockTypeId
                                 select b).SingleOrDefault();

                var stock = new StockEntity
                {
                    Name = GenerateName(stockCount, stockType),
                    Price = price,
                    Qunaitity = quanitity,
                    StockTypeId = stockTypeId
                };

                db.Stocks.Add(stock);
                db.SaveChanges();

                return stock;
            }
        }

        public string GenerateName(int stockCount, IStockType stockType)
        {
            return string.Format("{0}{1}", stockType.Name, stockCount + 1);
        }

        public IEnumerable<IStockType> GetStockTypes()
        {
            using (var db = new MyFundContext())
            { 
                var query = from b in db.StockTypes
                            orderby b.Name
                            select b;

                return query.ToList();
            }
        }
    }
}
