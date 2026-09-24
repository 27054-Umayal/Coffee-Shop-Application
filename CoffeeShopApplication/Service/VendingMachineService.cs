using CoffeeShopApplication.Core.Interfaces;
using CoffeeShopApplication.Core.Models;
using CoffeeShopApplication.Repository;

namespace CoffeeShopApplication.Service
{
    public class VendingMachineService : IVendingMachineService
    {
        private IVendingMachineRepo _vendingMachineRepo;

        public VendingMachineService(IVendingMachineRepo vendingMachineRepo)
        {
            this._vendingMachineRepo = vendingMachineRepo;
        }

        public bool AssignVendingMachine(Guid orderId)
        {
            return this._vendingMachineRepo.AssignVendingMachine(orderId);
        }

        public void ReleaseVendingMachine(Guid orderId)
        {
            this._vendingMachineRepo.ReleaseVendingMachine(orderId);
        }
    }
}
