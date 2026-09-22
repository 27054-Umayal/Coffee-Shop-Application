using CoffeeShopApplication.Core.Models;

namespace CoffeeShopApplication.Core.Interfaces
{
    public class IVendingMachineRepo
    {
        public IReadOnlyList<VendingMachine> GetVendingMachines();

        public bool AssignVendingMachine(int orderId);

        public void ReleaseVendingMachine(int orderId);
    }
}
