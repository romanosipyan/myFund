using myFund.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFund.Data.Context
{
    public class MyFundContext : DbContext
    {
        public DbSet<StockTypeEntity> StockTypes { get; set; }
        public DbSet<StockEntity> Stocks { get; set; }
    }
}
