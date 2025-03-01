using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WarehouseApp.Classes
{
   public class Product
    {
        public string Name { get; }
        public Money Price { get; private set; }
        public  Category Category { get;}

        public Product(string name, Money price, Category category)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Назва товару не може бути пустою або null", nameof(name));
            Name = name;
            Price = price;
            Category = category;
        }

        public void DecreasePrice(Money amount)
        {
            if (amount == null)
                throw new ArgumentNullException(nameof(amount));

            if (amount.WholeMoney * 100 + amount.FractionalMoney >= Price.WholeMoney * 100 + Price.FractionalMoney)
                throw new InvalidOperationException("Ціна не може бути менше нуля.");

            Price.Subtract(amount);
        }
        public void ApplyDiscount(decimal percentage)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentOutOfRangeException(nameof(percentage), "Відсоток має бути в діапазоні 0-100.");

            decimal totalCents = (Price.WholeMoney * 100) + Price.FractionalMoney;
            decimal discountAmount = totalCents * (percentage / 100m);
            long newTotalCents = (long)(totalCents - discountAmount);

            if (newTotalCents < 0)
                throw new InvalidOperationException("Ціна після знижки не може бути від'ємною.");

            long newWhole = newTotalCents / 100;
            int newFractional = (int)(newTotalCents % 100);
            Price = new Money((int)newWhole, newFractional, Price.Currency); 
        }

        public override string ToString()
        {
            return $"{Name} ({Category}) - {Price}";
        }
    }
}
