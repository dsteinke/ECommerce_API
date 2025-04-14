using System.Text.Json.Serialization;

namespace ECommerce.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {
        Pending = 0,
        Shipped = 1,
        Delivered = 2,
        Cancelled = 3
    }
}
