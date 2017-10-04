using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wallet;

namespace WalletTests
{
    [TestClass]
    public class WalletTests
    {
        [TestMethod]
        public void Put_PutNullValue_ThrowException()
        {
            // Arrange
            Wallet.Wallet wallet = new Wallet.Wallet();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => wallet.Put(null));
        }

        [TestMethod]
        public void Put_PutMoniesDifferentsCurrencies_MoniesPutted()
        {
            // Arrange
            decimal expectedFirstAmount = new Random().Next(1, int.MaxValue);
            Currency expectedFirstCurrency = Currency.AED;
            Money firstMoney = new Money(expectedFirstAmount, expectedFirstCurrency);
            decimal expectedSecondAmount = new Random().Next(1, int.MaxValue);
            Currency expectedSecondCurrency = Currency.AFN;
            Money secondMoney = new Money(expectedSecondAmount, expectedSecondCurrency);
            Wallet.Wallet wallet = new Wallet.Wallet();

            // Act
            wallet.Put(firstMoney);
            wallet.Put(secondMoney);

            // Assert
            Assert.IsNotNull(wallet.Monies);
            Assert.AreEqual(2, wallet.Monies.Count);
            Assert.IsTrue(wallet.Monies.Contains(firstMoney));
            Assert.IsTrue(wallet.Monies.Contains(secondMoney));
        }

        [TestMethod]
        public void Put_PutMoney_MoneyPutted()
        {
            // Arrange
            decimal expectedAmount = new Random().Next(1, int.MaxValue);
            Currency expectedCurrency = Currency.AED;
            Money money = new Money(expectedAmount, expectedCurrency);
            Wallet.Wallet wallet = new Wallet.Wallet();

            // Act
            wallet.Put(money);

            // Assert
            Assert.IsNotNull(wallet.Monies);
            Assert.IsTrue(wallet.Monies.Contains(money));
        }
        
        [TestMethod]
        public void SumWithMapReduce_PutOneMoney_SumEqualToMoneyAmount()
        {
            // Arrange
            decimal expectedAmount = new Random().Next(1, int.MaxValue);
            Currency expectedCurrency = Currency.AED;
            Money money = new Money(expectedAmount, expectedCurrency);
            Wallet.Wallet wallet = new Wallet.Wallet();
            wallet.Put(money);

            // Act
            Money actual = wallet.SumWithMapReduce(expectedCurrency);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedCurrency, actual.Currency);
            Assert.IsTrue(actual.Amount > 0);
            Assert.AreEqual(expectedAmount, actual.Amount);
        }

        [TestMethod]
        public void SumWithMapReduce_PutMoniesWithSameCurrency_SumEqualToMonies()
        {
            // Arrange
            decimal firstAmount = new Random().Next(1, int.MaxValue);
            decimal secondAmount = new Random().Next(1, int.MaxValue);
            Currency expectedCurrency = Currency.AED;
            Money firstMoney = new Money(firstAmount, expectedCurrency);
            Money secondMoney = new Money(secondAmount, expectedCurrency);
            Wallet.Wallet wallet = new Wallet.Wallet();
            wallet.Put(firstMoney);
            wallet.Put(secondMoney);

            // Act
            Money actual = wallet.SumWithMapReduce(expectedCurrency);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedCurrency, actual.Currency);
            Assert.IsTrue(actual.Amount > 0);
            Assert.AreEqual(firstAmount + secondAmount, actual.Amount);
        }

        [TestMethod]
        public void SumWithMapReduce_PutMoniesWithDifferentCurrency_SumEqualToMonies()
        {
            // Arrange
            Currency expectedCurrency = Currency.EUR;
            decimal firstAmount = new Random().Next(1, int.MaxValue);
            decimal secondAmount = new Random().Next(1, int.MaxValue);
            Currency firstCurrency = Currency.AED;
            Currency secondCurrency = Currency.AFN;
            Money firstMoney = new Money(firstAmount, firstCurrency);
            Money secondMoney = new Money(secondAmount, secondCurrency);
            Wallet.Wallet wallet = new Wallet.Wallet();
            wallet.Put(firstMoney);
            wallet.Put(secondMoney);

            // Act
            Money actual = wallet.SumWithMapReduce(expectedCurrency);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedCurrency, actual.Currency);
            Assert.IsTrue(actual.Amount > 0);
        }
    }
}
