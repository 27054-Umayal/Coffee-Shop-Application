namespace CoffeeShopApplication.Enums
{
    public enum OrderStatus
    {
        OrderPlaced = 1,
        WaitForVm,
        Preparing,
        S1Done,
        S2Done,
        S3Done,
        S4Done,
        S5Done,
        Completed,
        CancellationRequested,
        Cancelled,
    }
}
