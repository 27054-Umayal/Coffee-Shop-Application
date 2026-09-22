namespace CoffeeShopApplication.Core.Models
{
    public class VendingMachine
    {
        public int VMid { get; set; }

        public string VMName { get; set; }

        public string OrderIdAssigned { get; set; }

        public bool IsBusy { get; set; }
    }
}
