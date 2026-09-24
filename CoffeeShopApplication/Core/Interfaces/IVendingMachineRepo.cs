using CoffeeShopApplication.Core.Models;

namespace CoffeeShopApplication.Core.Interfaces
{
    public interface IVendingMachineRepo
    {
        public IReadOnlyList<VendingMachine> GetVendingMachines();

        public bool AssignVendingMachine(Guid orderId);

        public void ReleaseVendingMachine(Guid orderId);
    }
}
