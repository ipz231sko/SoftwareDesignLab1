using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Classes
{
    public class Reporting
    {
        private List<string> transactionLog;
        private IStockManager stockManager;
        private IInventoryReporter inventoryReporter;
        public Reporting(IStockManager stockManager, IInventoryReporter inventoryReporter)
        {
            this.stockManager = stockManager ?? throw new ArgumentNullException(nameof(stockManager));
            this.inventoryReporter = inventoryReporter ?? throw new ArgumentNullException(nameof(inventoryReporter));
            transactionLog = new List<string>();
        }

        public void RegisterIncome(Product product, int quantity)
        {
            stockManager.AddProduct(product, quantity);
            string logEntry = $"[Прибуток] {DateTime.Now}: Додано {quantity} шт. товару {product.Name}";
            transactionLog.Add(logEntry);
            Console.WriteLine(logEntry);
        }

        public void RegisterShipment(string productName, int quantity)
        {
            try
            {
                stockManager.RemoveProduct(productName, quantity);
                string logEntry = $"[Відвантаження] {DateTime.Now}: Відвантажено {quantity} шт. товару {productName}";
                transactionLog.Add(logEntry);
                Console.WriteLine(logEntry);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при відвантаженні товару: {ex.Message}");
            }
        }

        public void GenerateStockReport()
        {
            Console.WriteLine("\nЗвіт про залишки на складі:");
            inventoryReporter.DisplayStock();
        }

        public void GenerateTransactionReport()
        {
            Console.WriteLine("\nЗвіт про операції:");
            foreach (string log in transactionLog)
            {
                Console.WriteLine(log);
            }
        }
    }
}
