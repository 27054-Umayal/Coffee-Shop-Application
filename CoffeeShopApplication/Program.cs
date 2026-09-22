using CoffeeShopApplication.Controller;
using CoffeeShopApplication.Core.Interfaces;
using CoffeeShopApplication.Repository;
using CoffeeShopApplication.Service;

namespace Assignments
{
    public class Program
    {
        public static void Main()
        {
            IOrderRepo orderRepo = new OrderRepo();
            IProductRepo productRepo = new ProductsRepo();
            IVendingMachineRepo vendingMachineRepo = new VendingMachineRepo();
            IOrderService orderService = new OrderService(orderRepo);
            IProductService productService = new ProductsService(productRepo);
            IVendingMachineService vendingMachineService = new VendingMachineService(vendingMachineRepo);

            CoffeeShopController coffeeShopController = new CoffeeShopController(orderService, productService, vendingMachineService);
        }
    }
}