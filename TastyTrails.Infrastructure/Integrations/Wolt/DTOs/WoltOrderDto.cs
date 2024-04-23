using System.Text.Json.Serialization;

namespace TastyTrails.Infrastructure.Integrations.Wolt.DTOs
{
    public record WoltOrderDto
    {
        [JsonPropertyName("order_id")]
        public int Id { get; init; }

        [JsonPropertyName("items")]
        public List<WoltOrderItemDto> OrderItems { get; init; }
    }
}
