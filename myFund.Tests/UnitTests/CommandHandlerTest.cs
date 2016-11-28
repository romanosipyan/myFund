using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using myFund.Models;

namespace myFund.Tests.UnitTests
{
    [TestClass]
    public class CommandHandlerTest
    {

        [TestMethod]
        public void CanInitialize()
        {
            try
            {
                var handler = new Models.CommandHandler<object>(i => { }, i => false);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void CanExecute()
        {
            Func<int, bool> canExecute = ((i) => { return i == 0; });
            var handler = new Models.CommandHandler<int>(i => { }, canExecute);
            Assert.IsTrue(canExecute(10) == handler.CanExecute(10));
        }

        [TestMethod]
        public void Execute()
        {
            int testValue = 20;
            Action<int> execute = ((i) => 
            {
                i -= 10;
                Assert.IsTrue(i == 10);
            });
            var handler = new Models.CommandHandler<int>(execute, (i) => { return true; });
            handler.Execute(testValue);
        }
    }
}
