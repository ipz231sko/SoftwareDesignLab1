using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Classes
{
    public class Money
    {
        private int wholeMoney;
        private int fractionalMoney;

        public string Currency { get; private set; }

        public Money(int whole, int fractional, string currency)
        {
            if (fractional < 0 || fractional >= 100)
                throw new ArgumentException("Копійки повинні бути в діапазоні 0-99");

            wholeMoney = whole;
            fractionalMoney = fractional;
            Currency = currency;
        }

        public int WholeMoney => wholeMoney;
        public int FractionalMoney => fractionalMoney;

        public void SetMoney(int whole, int fractional)
        {
            if(fractional < 0 || fractional >= 100)
                throw new ArgumentException("Копійки повинні бути в діапазоні 0-99");

            wholeMoney = whole;
            fractionalMoney = fractional;
        }

        public override string ToString()
        {
            return $"{wholeMoney} {Currency} {fractionalMoney:D2} коп.";
        }

        public void Add(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Неможливо додати різні валюти.");

            wholeMoney += other.wholeMoney;
            fractionalMoney += other.fractionalMoney;

            if(fractionalMoney >= 100)
            {
                wholeMoney += fractionalMoney / 100;
                fractionalMoney %= 100;
            }
        }

        public void Subtract(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Неможливо відняти різні валюти.");
            
            int totalCents1 = wholeMoney * 100 + fractionalMoney;
            int totalCents2 = other.wholeMoney * 100 + other.fractionalMoney;

            if (totalCents1 < totalCents2)
                throw new InvalidOperationException("Не можна мати від'ємний баланс.");

            int resultCents = totalCents1 - totalCents2;
            wholeMoney = resultCents / 100;
            fractionalMoney = resultCents % 100;
        }
    }
    public class USD : Money
    {
        public USD(int whole, int cents) : base(whole, cents, "USD")
        {
        }
    }
    public class EUR : Money
    {
        public EUR(int whole, int cents) : base(whole, cents, "EUR")
        {
        }
    }
    public class UAH : Money
    {
        public UAH(int whole, int cents) : base(whole, cents, "UAH")
        {
        }
    }
}
