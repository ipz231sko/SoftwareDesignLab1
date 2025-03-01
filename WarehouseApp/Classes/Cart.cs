using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Classes
{
    public class Cart
    {
        private List<(Product product, int quantity)> items;
        public Cart()
        {
            items = new List<(Product product, int quantity)>();
        }
        public void AddToCart(Product product, int quantity)
        {
            items.Add((product, quantity));
            Console.WriteLine($"Додано {quantity} шт. товару {product.Name} до кошика");
        }
        public void RemoveFromCart(string productName)
        {
            items.RemoveAll(item => item.product.Name == productName);
            Console.WriteLine($"Товар {productName} видалено з корзини.");
        }
        public void DisplayCart()
        {
            Console.WriteLine("Корзина покупок:");
            foreach (var item in items)
            {
                Console.WriteLine($"{item.product.Name} - {item.quantity} шт. за {item.product.Price}");
            }

        }
        public Money CalculateTotal()
        {
            int totalWhole = 0;
            int totalCents = 0;
            string currency = items.Count > 0 ? items[0].product.Price.Currency : "UAH";

            foreach (var item in items)
            {
                totalWhole += item.product.Price.WholeMoney * item.quantity;
                totalCents += item.product.Price.FractionalMoney * item.quantity;
            }
            totalWhole += totalCents / 100;
            totalCents = totalCents % 100;
            return new Money(totalWhole, totalCents, currency);
        }
    }
}