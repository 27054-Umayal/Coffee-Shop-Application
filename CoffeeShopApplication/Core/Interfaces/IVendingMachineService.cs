using CoffeeShopApplication.Repository;

namespace CoffeeShopApplication.Core.Interfaces
{
    public class IVendingMachineService
    {
        public bool AssignVendingMachine(int orderId);

        public void ReleaseVendingMachine(int orderId);
    }
}
