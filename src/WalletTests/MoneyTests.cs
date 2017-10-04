using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wallet;

namespace WalletTests
{
    [TestClass]
    public class MoneyTests
    {
        [TestMethod]
        [DataRow("-100,1")]
        [DataRow("-100")]
        [DataRow("-1")]
        [DataRow("0")]
        [DataRow("0,0")]
        public void Ctor_InvalidAmount_ThrowException(string amount)
        {
            // Arrange
            decimal realAmount = decimal.Parse(amount);
            Currency currency = Currency.AED;

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Money(realAmount, currency));
        }

        [TestMethod]
        public void Ctor_InvalidCurrency_ThrowException()
        {
            // Arrange
            decimal amount = new Random().Next(1, int.MaxValue);
            Currency currency = default(Currency);

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => new Money(amount, currency));
        }

        [TestMethod]
        [DataRow("1", "0,1")]
        [DataRow("1", "1,1")]
        [DataRow("100", "100,01")]
        public void Equals_DifferentAmounts_MoniesNotEqual(string firstAmount, string secondAmount)
        {
            // Arrange
            decimal realFirstAmount = decimal.Parse(firstAmount);
            decimal realSecondAmount = decimal.Parse(secondAmount);
            Currency currency = Currency.AED;
            var firstMoney = new Money(realFirstAmount, currency);
            var secondMoney = new Money(realSecondAmount, currency);

            // Act
            bool actual = firstMoney.Equals(secondMoney);

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        [DataRow(Currency.AED, Currency.AFN)]
        [DataRow(Currency.EUR, Currency.DOP)]
        public void Equals_DifferentCurrencies_MoniesNotEqual(Currency firstCurrency, Currency secondCurrency)
        {
            // Arrange
            decimal amount = new Random().Next(1, int.MaxValue);
            var firstMoney = new Money(amount, firstCurrency);
            var secondMoney = new Money(amount, secondCurrency);

            // Act
            bool actual = firstMoney.Equals(secondMoney);

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        [DataRow("1", "1", Currency.AED, Currency.AED)]
        [DataRow("1", "1,00", Currency.AED, Currency.AED)]
        [DataRow("100", "100,0", Currency.AED, Currency.AED)]
        public void Equals_SameMonies_MoniesEqual(string firstAmount, string secondAmount, Currency firstCurrency, Currency secondCurrency)
        {
            // Arrange
            decimal realFirstAmount = decimal.Parse(firstAmount);
            decimal realSecondAmount = decimal.Parse(secondAmount);
            var firstMoney = new Money(realFirstAmount, firstCurrency);
            var secondMoney = new Money(realSecondAmount, secondCurrency);

            // Act
            bool actual = firstMoney.Equals(secondMoney);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        [DataRow("1", "0,1")]
        [DataRow("2", "1")]
        [DataRow("100", "100,01")]
        public void CompareTo_DifferentAmounts_MoniesNotEqual(string firstAmount, string secondAmount)
        {
            // Arrange
            int equalityValue = 0;
            decimal realFirstAmount = decimal.Parse(firstAmount);
            decimal realSecondAmount = decimal.Parse(secondAmount);
            Currency currency = Currency.AED;
            var firstMoney = new Money(realFirstAmount, currency);
            var secondMoney = new Money(realSecondAmount, currency);

            // Act
            int actual = firstMoney.CompareTo(secondMoney);

            // Assert
            Assert.AreNotEqual(equalityValue, actual);
        }

        [TestMethod]
        [DataRow(Currency.AED, Currency.AFN)]
        [DataRow(Currency.EUR, Currency.DOP)]
        public void CompareTo_DifferentCurrencies_MoniesNotEqual(Currency firstCurrency, Currency secondCurrency)
        {
            // Arrange
            decimal amount = new Random().Next(1, int.MaxValue);
            var firstMoney = new Money(amount, firstCurrency);
            var secondMoney = new Money(amount, secondCurrency);

            // Act
            Assert.ThrowsException<InvalidOperationException>(() => firstMoney.CompareTo(secondMoney), $"Cannot compare {firstCurrency} and {secondCurrency}.");
        }

        [TestMethod]
        [DataRow("1", "1", Currency.AED, Currency.AED)]
        [DataRow("1", "1,00", Currency.AED, Currency.AED)]
        [DataRow("100", "100,0", Currency.AED, Currency.AED)]
        public void CompateTo_SameMonies_MoniesEqual(string firstAmount, string secondAmount, Currency firstCurrency, Currency secondCurrency)
        {
            // Arrange
            int equalityValue = 0;
            decimal realFirstAmount = decimal.Parse(firstAmount);
            decimal realSecondAmount = decimal.Parse(secondAmount);
            var firstMoney = new Money(realFirstAmount, firstCurrency);
            var secondMoney = new Money(realSecondAmount, secondCurrency);

            // Act
            int actual = firstMoney.CompareTo(secondMoney);

            // Assert
            Assert.AreEqual(equalityValue, actual);
        }
    }
}