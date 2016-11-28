using myFund.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFund.Domain.Repositories
{
    public interface IStockRepository
    {
        IEnumerable<IStock> GetStocks();

        IEnumerable<IStockType> GetStockTypes();

        IStock AddStock(int stockTypeId, decimal price, int quanitity);

        string GenerateName(int stockCount, IStockType stockType);
    }
}
