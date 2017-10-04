using System;
using System.Collections.Generic;
using System.Linq;

namespace Wallet
{
    public class Wallet
    {
        public List<Money> Monies { get; } = new List<Money>();

        public void Put(Money money)
        {
            if (money == null) throw new ArgumentNullException(nameof(money));
            if (money.Currency.Equals(default(Currency))) throw new ArgumentException($"Cannot put default value of {nameof(money.Currency)}.");

            this.Monies.Add(new Money(money.Amount, money.Currency));
        }

        public Money SumWithMapReduce(Currency currency)
        {
            return Monies.AsParallel().Aggregate((firstMoney, secondMoney) => this.Exchange(firstMoney.Amount, firstMoney.Currency, currency) + this.Exchange(secondMoney.Amount, secondMoney.Currency, currency));
        }

        public Money Sum(Currency currency)
        {
            return new Money(Monies.Select(m => Exchange(m.Amount, m.Currency, currency)).Sum(m => m.Amount), currency);
        }

        /// <summary>
        /// Radom exchage.
        /// You can refactor this method to get a real exchange through an api.
        /// </summary>
        /// <param name="amount">The amout.</param>
        /// <param name="from">The current currency.</param>
        /// <param name="to">The target currency.</param>
        /// <returns></returns>
        private Money Exchange(decimal amount, Currency from, Currency to)
        {
            if (from != to) amount = amount * Convert.ToDecimal(new Random().NextDouble() * (2.2 - 0.1) + 0.1);
            return new Money(amount, to);
        }
    }
}
