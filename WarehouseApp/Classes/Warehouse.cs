using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Classes
{
    public interface IStockManager
    {
        void AddProduct(Product product, int quantity);
        void RemoveProduct(string productName, int quantity);
        void UpdateStock(string productName, int newQuantity);
    }

    public interface IInventoryReporter
    {
        void DisplayStock();
        void DisplayInventory();
    }

    public interface IShippingManager
    {
        void ShipProduct(string productName, int quantity, string destination);
    }

    public class Warehouse : IStockManager, IInventoryReporter, IShippingManager
    {
        public List<Product> products;
        private Dictionary<string, int> stock;

        public Warehouse()
        {
            products = new List<Product>();
            stock = new Dictionary<string, int>();
        }

        public void AddProduct(Product product, int quantity)
        {
            if(quantity <= 0)
                throw new ArgumentException("Кількість товару не може бути менше або дорівнювати нулю", nameof(quantity));

            products.Add(product);
            if (stock.ContainsKey(product.Name))
                stock[product.Name] += quantity;
            else
                stock[product.Name] = quantity;

            Console.WriteLine($"Товар {product.Name} додано на склад у кількості {quantity} шт.");
        }

        public void RemoveProduct(string productName, int quantity)
        {
            if(!stock.ContainsKey(productName) || stock[productName] < quantity)
                throw new InvalidOperationException("На складі недостатньо товару");

            stock[productName] -= quantity;
            if (stock[productName] == 0)
                stock.Remove(productName);

            Console.WriteLine($"Товар {productName} відвантажено у кількості {quantity} шт.");
        }

        public void UpdateStock(string productName, int newQuantity)
        {
            if (!stock.ContainsKey(productName))
                throw new KeyNotFoundException("Товару з такою назвою не знайдено на складі");

            if (newQuantity < 0)
                throw new ArgumentException("Кількість товару не може бути від'ємною");

            stock[productName] = newQuantity;
            Console.WriteLine($"Оновлено кількість товару {productName}: {newQuantity} шт.");
        }
        public Product GetProductInfo(string productName)
        {
            return products.FirstOrDefault(p => p.Name == productName);
        }
        public void DisplayStock()
        {
            Console.WriteLine("Складські залишки:");
            foreach (var item in stock)
            {
                Product product = GetProductInfo(item.Key);
                Console.WriteLine($"{item.Key} - {item.Value} шт. (Ціна: {product.Price})");
            }
        }
        public void DisplayInventory()
        {
            Console.WriteLine("Інвентаризація складу:");
            foreach (var item in stock)
            {
                Product product = GetProductInfo(item.Key);
                if(product != null)
                {
                    Console.WriteLine($"Товар: {product.Name}");
                    Console.WriteLine($"Категорія: {product.Category.Name}");
                    Console.WriteLine($"Ціна: {product.Price}");
                    Console.WriteLine($"Кількість у наявності: {item.Value} шт.");
                    Console.WriteLine(new string('-', 30));
                }
            }
        }
        public void ShipProduct(string productName, int quantity, string destination)
        {
            if (!stock.ContainsKey(productName) || stock[productName] < quantity)
                throw new InvalidOperationException("На складі недостатньо товару");
            stock[productName] -= quantity;
            if (stock[productName] == 0)
                stock.Remove(productName);
            Console.WriteLine($"Товар {productName} відвантажено у кількості {quantity} шт. до міста {destination}");
        }
    }
}
