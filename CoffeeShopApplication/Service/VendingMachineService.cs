using CoffeeShopApplication.Core.Models;

namespace CoffeeShopApplication.Service
{
    public class VendingMachineService
    {
        public VendingMachineService()
        {

        }

        public bool CheckIfVendingMachineBusy(int vmId)
        {
            VendingMachine vendingMachine = this._vendingMachineRepo.GetVendingMachines();
            VendingMachine vendingMachineId = this._vendingMachines.FirstOrDefault(vendingMachineId => vendingMachineId.VmId == vmId);
            return vendingMachineId.IsBusy == true;
        }
    }
}
