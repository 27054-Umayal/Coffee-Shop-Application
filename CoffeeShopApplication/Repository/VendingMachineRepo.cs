using CoffeeShopApplication.Core.Interfaces;
using CoffeeShopApplication.Core.Models;

namespace CoffeeShopApplication.Repository
{
    public class VendingMachineRepo : IVendingMachineRepo
    {
        private readonly List<VendingMachine> _vendingMachines;

        public VendingMachineRepo()
        {
            this._vendingMachines = new List<VendingMachine>
            {
                new VendingMachine { VMid = Guid.NewGuid(), VMName = "VM1", OrderIdAssigned = null, IsBusy = false },
                new VendingMachine { VMid = Guid.NewGuid(), VMName = "VM2", OrderIdAssigned = null, IsBusy = false },
                new VendingMachine { VMid = Guid.NewGuid(), VMName = "VM3", OrderIdAssigned = null, IsBusy = false },
            };
        }

        public IReadOnlyList<VendingMachine> GetVendingMachines()
        {
            return this._vendingMachines;
        }

        public bool AssignVendingMachine(Guid orderId)
        {
            for (int i = 0; i < this._vendingMachines.Count; i++)
            {
                if (!this._vendingMachines[i].IsBusy)
                {
                    this._vendingMachines[i].OrderIdAssigned = orderId;
                    this._vendingMachines[i].IsBusy = true;
                    return true;
                }
            }

            return false;
        }

        public void ReleaseVendingMachine(Guid orderId)
        {
            foreach (VendingMachine vendingMachine in this._vendingMachines)
            {
                if (vendingMachine.OrderIdAssigned == orderId)
                {
                    vendingMachine.OrderIdAssigned = null;
                    vendingMachine.IsBusy = false;
                }
            }
        }
    }
}
