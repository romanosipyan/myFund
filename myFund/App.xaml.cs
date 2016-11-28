using Autofac;
using Autofac.Core;
using myFund.Data.Context;
using myFund.Data.Repositories;
using myFund.Domain.Repositories;
using myFund.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace myFund
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IContainer Container { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.DataBaseMigration();
            this.RegisterDependencies();            
        }

        private void DataBaseMigration()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyFundContext, MyFundContextMigrationConfig>());
        }

        private void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<StockRepository>().As<IStockRepository>();
            builder.RegisterType<StocksViewModel>().As<IStocksViewModel>().InstancePerLifetimeScope();
            Container = builder.Build();
        }
    }
}
