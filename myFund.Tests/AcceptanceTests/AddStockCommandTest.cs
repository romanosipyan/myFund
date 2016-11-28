using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using myFund.ViewModels;
using Moq;
using myFund.Domain.Repositories;
using myFund.Data.Entities;
using myFund.Models;

namespace myFund.Tests.AcceptanceTests
{
    [TestClass]
    public class AddStockCommandTest
    {
        [TestMethod]
        public void HasAddCommand()
        {
            Assert.IsTrue(typeof(IStocksViewModel).GetProperty("AddStockCommand") != null);
        }

        [TestMethod]
        public void StockAddCommand()
        {
            var repoMock = new Mock<IStockRepository>(MockBehavior.Loose);
           
            repoMock.Setup(r => r.GetStockTypes()).Returns(new StockTypeEntity[] { new StockTypeEntity { Name = "Bond" }, new StockTypeEntity { Name = "Equity" } });
            repoMock.Setup(r => r.GetStocks()).Returns(new StockEntity[] { new StockEntity { Name = "Bond1", Price = 15, Qunaitity = 5, StockType = new StockTypeEntity { Name = "Bond" } } });
            var model = new StocksViewModel(repoMock.Object);

            var newStock = new StockTypeModel { Name = "Equity", Price = 10, Quantity = 5 };
            repoMock.Setup(r => r.AddStock(It.IsAny<int>(), It.IsAny<decimal>(), It.IsAny<int>())).Returns(new StockEntity { Name = newStock.Name, Price = newStock.Price, Qunaitity = newStock.Quantity, StockType = new StockTypeEntity { Name = "Equity" } });
            model.AddStockCommand.Execute(newStock);

            Assert.IsTrue(model.Stocks.Count == 2);
        }

        [TestMethod]
        public void ShouldHavePriceQuantity()
        {
            Assert.IsTrue(typeof(StockTypeModel).GetProperty("Price") != null);
            Assert.IsTrue(typeof(StockTypeModel).GetProperty("Quantity") != null);

            var newStock = new StockTypeModel { Name = "Equity", Price = 10, Quantity = 5 };
            Assert.IsTrue(newStock.Validate());

            var failStock = new StockTypeModel { Name = "Equity"};
            Assert.IsFalse(failStock.Validate());
        }
    }
}
