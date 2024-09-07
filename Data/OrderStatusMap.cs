using Restaurants_Platform.Models;
using System.Reflection;

namespace Restaurants_Platform.Data;

public static class OrderStatusMap
{
    public static class Preparing 
    {
        public static int Id  => 1;
        public static string Name  => "Preparing";
    }

    public static class OutForDelivery
    {
        public static int Id => 2;
        public static string Name => "Out For Delivery";
    }

    public static class Delivered
    {
        public static int Id => 3;
        public static string Name => "Delivered";
    }

    public static class Canceled
    {
        public static int Id => 4;
        public static string Name => "Canceled";
    }

    // Following code automaticlly applies changes of code status. You still need to do EF add-migration though to reflect changes on the database
    public static List<OrderStatus> GetAllOrderStatuses()
    {
        return typeof(OrderStatusMap)
            .GetNestedTypes(BindingFlags.Public | BindingFlags.Static)
            .Select(t => new OrderStatus
            {
                Id = (int)t.GetProperty(nameof(Preparing.Id))!.GetValue(null)!,
                Name = (string)t.GetProperty(nameof(Preparing.Name))!.GetValue(null)!
            })
            .ToList();
    }
}