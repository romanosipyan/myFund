using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using myFund.Models;
using myFund.Data.Repositories;
using myFund.Data.Entities;
using Moq;
using myFund.ViewModels;
using myFund.Domain.Repositories;
using System.Linq;

namespace myFund.Tests.AcceptanceTests
{
    [TestClass]
    public class StocksGridTest
    {
        [TestMethod]
        public void HasAllNeededData()
        {
            Assert.IsTrue(typeof(StockModel).GetProperty("Type") != null);
            Assert.IsTrue(typeof(StockModel).GetProperty("Name") != null);
            Assert.IsTrue(typeof(StockModel).GetProperty("Price") != null);
            Assert.IsTrue(typeof(StockModel).GetProperty("Quantity") != null);
            Assert.IsTrue(typeof(StockModel).GetProperty("MarketValue") != null);
            Assert.IsTrue(typeof(StockModel).GetProperty("TransactionCost") != null);
            Assert.IsTrue(typeof(StockModel).GetProperty("Weight") != null);
        }

        [TestMethod]
        public void StockNameTest()
        {
            var repo = new StockRepository();
            var name = repo.GenerateName(3, new StockTypeEntity { Name = "Bond" });
            Assert.IsTrue(name.Equals("Bond4"));
        }

        [TestMethod]
        public void MarketValueTest()
        {
            var newStock = new StockModel { Name = "Equity", Price = 10, Quantity = 5 };
            Assert.IsTrue(newStock.MarketValue == newStock.Price * newStock.Quantity);
        }

        [TestMethod]
        public void TransactionCost()
        {
            var equityStock = new StockModel { Name = "Equity1", Price = 10, Quantity = 5, Type = new StockTypeModel { Name = "Equity" } };
            var bondStock = new StockModel { Name = "Bond1", Price = 50, Quantity = 25, Type = new StockTypeModel { Name = "Bond" } };
            Assert.IsTrue(equityStock.TransactionCost == (equityStock.MarketValue * (decimal)0.5 / 100));
            Assert.IsTrue(bondStock.TransactionCost == (bondStock.MarketValue * 2 / 100));
        }

        [TestMethod]
        public void StockWeight()
        {
            var repoMock = new Mock<IStockRepository>(MockBehavior.Loose);

            repoMock.Setup(r => r.GetStockTypes()).Returns(new StockTypeEntity[] { new StockTypeEntity { Name = "Bond" }, new StockTypeEntity { Name = "Equity" } });
            repoMock.Setup(r => r.GetStocks()).Returns(
                new StockEntity[]
                {
                    new StockEntity { Name = "Bond1", Price = 15, Qunaitity = 5, StockType = new StockTypeEntity { Name = "Bond" } },
                    new StockEntity { Name = "Bond2", Price = 3, Qunaitity = 25, StockType = new StockTypeEntity { Name = "Bond" } }
                });

            var model = new StocksViewModel(repoMock.Object);

            var totalMarketValue = model.Stocks.Sum(s => s.MarketValue);

            var equityStock = model.Stocks.First();
            Assert.IsTrue(equityStock.Weight == (equityStock.MarketValue * 100 / totalMarketValue));
        }

        [TestMethod]
        public void RedStockColor()
        {
            var repoMock = new Mock<IStockRepository>(MockBehavior.Loose);

            repoMock.Setup(r => r.GetStockTypes()).Returns(new StockTypeEntity[] { new StockTypeEntity { Name = "Bond" }, new StockTypeEntity { Name = "Equity" } });
            repoMock.Setup(r => r.GetStocks()).Returns(
                new StockEntity[]
                {
                    new StockEntity { Name = "Bond1", Price = 150000, Qunaitity = 50, StockType = new StockTypeEntity { Name = "Bond" } },
                    new StockEntity { Name = "Bond2", Price = 3, Qunaitity = 25, StockType = new StockTypeEntity { Name = "Bond" } }
                });

            var model = new StocksViewModel(repoMock.Object);

            Assert.IsTrue(model.Stocks[0].IsNegative);
            Assert.IsFalse(model.Stocks[1].IsNegative);
        }

        [TestMethod]
        public void TotalsTest()
        {
            var repoMock = new Mock<IStockRepository>(MockBehavior.Loose);

            repoMock.Setup(r => r.GetStockTypes()).Returns(new StockTypeEntity[] { new StockTypeEntity { Name = "Bond" }, new StockTypeEntity { Name = "Equity" } });
            repoMock.Setup(r => r.GetStocks()).Returns(
                new StockEntity[]
                {
                    new StockEntity { Name = "Bond1", Price = 15, Qunaitity = 5, StockType = new StockTypeEntity { Name = "Bond" } },
                    new StockEntity { Name = "Bond2", Price = 3, Qunaitity = 25, StockType = new StockTypeEntity { Name = "Bond" } },
                    new StockEntity { Name = "Equity1", Price = 32, Qunaitity = 55, StockType = new StockTypeEntity { Name = "Equity" } }
                });

            var model = new StocksViewModel(repoMock.Object);

            var totalMarketValue = model.Stocks.Sum(s => s.MarketValue);
            var totalWeight = model.Stocks.Sum(s => s.Weight);

            Assert.IsTrue(model.AllTotals.TotalMarketValue == totalMarketValue);
            Assert.IsTrue(model.AllTotals.TotalNumbers == model.Stocks.Count);
            Assert.IsTrue(model.AllTotals.TotalWeight == totalWeight);

            var equityTotalMarketValue = model.Stocks.Where(s=>s.Type.Name == "Equity").Sum(s => s.MarketValue);
            var equityTotalWeight = model.Stocks.Where(s => s.Type.Name == "Equity").Sum(s => s.Weight);

            Assert.IsTrue(model.EquityTotals.TotalMarketValue == equityTotalMarketValue);
            Assert.IsTrue(model.EquityTotals.TotalNumbers == model.Stocks.Where(s => s.Type.Name == "Equity").Count());
            Assert.IsTrue(model.EquityTotals.TotalWeight == equityTotalWeight);

            var bondTotalMarketValue = model.Stocks.Where(s => s.Type.Name == "Bond").Sum(s => s.MarketValue);
            var bondTotalWeight = model.Stocks.Where(s => s.Type.Name == "Bond").Sum(s => s.Weight);

            Assert.IsTrue(model.BondTotals.TotalMarketValue == bondTotalMarketValue);
            Assert.IsTrue(model.BondTotals.TotalNumbers == model.Stocks.Where(s => s.Type.Name == "Bond").Count());
            Assert.IsTrue(model.BondTotals.TotalWeight == bondTotalWeight);
        }
    }
}
