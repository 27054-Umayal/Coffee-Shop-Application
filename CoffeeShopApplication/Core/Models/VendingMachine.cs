namespace CoffeeShopApplication.Core.Models
{
    public class VendingMachine
    {
        public Guid VMid { get; set; }

        public string VMName { get; set; }

        public Guid? OrderIdAssigned { get; set; }

        public bool IsBusy { get; set; }
    }
}
