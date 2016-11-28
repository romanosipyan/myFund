using myFund.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFund.Data.Entities
{
    public class StockEntity : IStock
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Qunaitity { get; set; }

        public virtual StockTypeEntity StockType { get; set; }

        [NotMapped]
        public IStockType Type
        {
            get { return this.StockType; }
        }

        public int StockTypeId { get; set; }
    }
}
