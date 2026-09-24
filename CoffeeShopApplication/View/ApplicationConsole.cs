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
            Console.WriteLine(
                $"{"No",-5} {"Order Id",-10} {"ProductId",-10} {"Status",-10}");

            Console.WriteLine(new string('-', 40));

            for (int i = 0; i < orders.Count; i++)
            {
                Console.WriteLine(
                    $"{i + 1,-5} " +
                    $"{orders[i].OrderId.ToString()[..8],-10}" +
                    $"{orders[i].ProductId.ToString()[..8],-10} " +
                    $"{orders[i].OrderStatus,-10}");
            }
        }
    }
}
