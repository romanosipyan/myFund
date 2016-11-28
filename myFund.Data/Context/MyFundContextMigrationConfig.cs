using myFund.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFund.Data.Context
{
    public class MyFundContextMigrationConfig :  DbMigrationsConfiguration<MyFundContext>
    {
        public MyFundContextMigrationConfig()
        {
            AutomaticMigrationsEnabled = true;
        }
        protected override void Seed(MyFundContext context)
        {     
            var equity = new StockTypeEntity { Name = "Equity" };
            var bond = new StockTypeEntity { Name = "Bond" };

            context.StockTypes.AddOrUpdate(x => x.Name, equity, bond);
            context.SaveChanges();
        }
    }
}
