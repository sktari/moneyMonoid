using System;
using System.Diagnostics;
using Wallet;

namespace ConsoleApp
{
    class Program
    {
        static Random _generator = new Random();

        static void Main(string[] args)
        {
            Wallet.Wallet wallet = new Wallet.Wallet();
            
            // Put random monies in wallet.
            for (int counter = 0; counter < 1000000; counter++)
            {
                wallet.Put(new Money(GetRandomAmout(), GetRandomCurrency()));
            }

            // Stopwatch with mapreduce.
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Money money = wallet.SumWithMapReduce(Currency.EUR);
            watch.Stop();

            // Stopwatch without mapreduce
            Stopwatch watch2 = new Stopwatch();
            watch2.Start();
            Money money2 = wallet.Sum(Currency.EUR);
            watch2.Stop();

            Console.WriteLine($"Sum with MapReduce");
            Console.WriteLine($"{money.Amount}{money.Currency}");
            Console.WriteLine($"{watch.Elapsed.Minutes} Minutes. {watch.Elapsed.Seconds} Seconds. {watch.Elapsed.Milliseconds} Milliseconds.");

            Console.WriteLine($"Sum without MapReduce");
            Console.WriteLine($"{money2.Amount}{money2.Currency}");
            Console.WriteLine($"{watch2.Elapsed.Minutes} Minutes. {watch2.Elapsed.Seconds} Seconds. {watch2.Elapsed.Milliseconds} Milliseconds.");

            Console.ReadKey();
        }

        static decimal GetRandomAmout()
        {
            return _generator.Next(1, int.MaxValue);
        }

        static Currency GetRandomCurrency()
        {
            Array values = Enum.GetValues(typeof(Currency));
            return (Currency)values.GetValue(_generator.Next(values.Length));
        }
    }
}
