using CoffeeShopApplication.Core.Models;

namespace CoffeeShopApplication.Repository
{
    public class VendingMachineRepo
    {
        private readonly List<VendingMachine> _vendingMachines;

        public VendingMachineRepo()
        {
            this._vendingMachines = new List<VendingMachine>();
        }

        public IReadOnlyList GetVendingMachines()
        {
            return this._vendingMachines;
        }

        public bool AssignVendingMachine(int orderId)
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

        public void ReleaseVendingMachine(int orderId)
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
