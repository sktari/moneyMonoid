using System;
using System.Collections.Generic;

namespace Wallet
{
    public sealed class Money : IEquatable<Money>, IComparable, IComparable<Money>
    {
        public readonly Currency Currency;
        public readonly decimal Amount;
        public Money(decimal amount, Currency currency)
        {
            if (amount <= 0) throw new ArgumentException($"The {nameof(amount)} must be greater than 0.");
            if (currency == default(Currency)) throw new ArgumentException($"The {nameof(currency)} must have a correct value.");

            Amount = amount;
            Currency = currency;
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj))
                return 1;

            if (ReferenceEquals(this, obj))
                return 0;

            if (obj.GetType() != this.GetType())
                throw new InvalidOperationException($"Cannot convert object of type '{obj.GetType().FullName}' to '{this.GetType().FullName}'.");

            return this.CompareTo(obj as Money);
        }

        public int CompareTo(Money other)
        {
            if (ReferenceEquals(null, other))
                return 1;

            if (this.Currency != other.Currency)
                throw new InvalidOperationException($"Cannot compare {this.Currency} and {other.Currency}.");

            return this.Amount.CompareTo(other.Amount);
        }

        public bool Equals(Money other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            return EqualityComparer<decimal>.Default.Equals(this.Amount, other.Amount) &&
                Equals(this.Currency, other.Currency);
        }

        public static Money operator +(Money firstMoney, Money secondMoney)
        {
            if (firstMoney.Currency != secondMoney.Currency) throw new ArgumentException("Currencies must be same.");
            return new Money(firstMoney.Amount + secondMoney.Amount, firstMoney.Currency);
        }
    }
}