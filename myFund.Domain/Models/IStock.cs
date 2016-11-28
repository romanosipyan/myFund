using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFund.Domain.Models
{
    public interface IStock
    {
        int Id { get; set; }

        string Name { get; set; }

        int StockTypeId { get; set; }

        decimal Price { get; set; }

        int Qunaitity { get; set; }

        IStockType Type { get; }
    }
}
