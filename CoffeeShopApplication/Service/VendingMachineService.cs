using CoffeeShopApplication.Core.Models;
using CoffeeShopApplication.Repository;

namespace CoffeeShopApplication.Service
{
    public class VendingMachineService
    {
        private VendingMachineRepo _vendingMachineRepo;

        public VendingMachineService(VendingMachineRepo vendingMachineRepo)
        {
            this._vendingMachineRepo = vendingMachineRepo;
        }

        public bool AssignVendingMachine(int orderId)
        {
            return this._vendingMachineRepo.AssignVendingMachine(orderId);
        }

        public void ReleaseVendingMachine(int orderId)
        {
            this._vendingMachineRepo.ReleaseVendingMachine(orderId);
        }
    }
}
