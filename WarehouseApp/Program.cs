using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseApp.Classes;

namespace WarehouseApp
{
    class Program
    {
        static void Main()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)
            System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            Category electronics = new Category("Електроніка");
            Category food = new Category("Їжа");

            Product laptop = new Product("Ноутбук", new USD(1200, 99), electronics);
            Product apple = new Product("Яблуко", new UAH(20, 50), food);
            Product phone = new Product("Смартфон", new USD(800, 49), electronics);

            Console.WriteLine("\nТест 1: Додавання товарів у кошик ---");
            Cart cart = new Cart();
            cart.AddToCart(laptop, 2);
            cart.AddToCart(apple, 5);
            cart.DisplayCart();

            Console.WriteLine("\nТест 2: Видалення товару з кошика ---");
            cart.RemoveFromCart("Яблуко");
            cart.DisplayCart();

            Console.WriteLine("\nТест 3: Обчислення загальної вартості кошика ---");
            Money total = cart.CalculateTotal();
            Console.WriteLine($"Загальна вартість покупки: {total}");

            Console.WriteLine("\nТест 4: Додавання товарів на склад ---");
            IStockManager stockManager = new Warehouse();
            IInventoryReporter inventoryReporter = (IInventoryReporter)stockManager;
            IShippingManager shippingManager = (IShippingManager)stockManager;

            stockManager.AddProduct(laptop, 10);
            stockManager.AddProduct(phone, 5);
            inventoryReporter.DisplayInventory();

            Console.WriteLine("\nТест 5: Відвантаження товарів та перевірка залишків");
            shippingManager.ShipProduct("Ноутбук", 2, "Харків");
            inventoryReporter.DisplayInventory();


            Console.WriteLine("\nТест 6: Зміна ціни товару");
            Console.WriteLine($"Стара ціна смартфона: {phone.Price}");
            phone.ApplyDiscount(10);
            Console.WriteLine($"Нова ціна смартфона після знижки 10%: {phone.Price}");

            Console.WriteLine("\nТест 7: Операції з валютою");
            Money money1 = new USD(50, 25);
            Money money2 = new USD(20, 75);
            money1.Add(money2);
            Console.WriteLine($"Результат додавання: {money1}");
            try
            {
                Money uahMoney = new UAH(100, 50);
                money1.Add(uahMoney);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}
