using CoffeeShopApplication.Core.Models;

namespace CoffeeShopApplication.View
{
    public static class ApplicationConsole
    {
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static bool ReadIntInput(string promptMessage, out int intInput)
        {
            DisplayMessage(promptMessage);
            string? input = Console.ReadLine();
            return int.TryParse(input, out intInput);
        }

        public static void ViewMenu(Type enumType)
        {
            foreach (var choice in Enum.GetValues(enumType))
            {
                DisplayMessage($"{(int)choice}. {choice}");
            }
        }

        public static void DisplayOrders(IReadOnlyList<Orders> orders)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                ApplicationConsole.DisplayMessage(
                    $"{i + 1}. Order ID: {orders[i].OrderId} ProductId: {orders[i].ProductId}");
            }
        }
    }
}
