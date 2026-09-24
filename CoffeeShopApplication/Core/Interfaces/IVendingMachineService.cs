namespace CoffeeShopApplication.Core.Interfaces
{
    public interface IVendingMachineService
    {
        public bool AssignVendingMachine(Guid orderId);

        public void ReleaseVendingMachine(Guid orderId);
    }
}
