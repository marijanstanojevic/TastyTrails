using System.Text.Json.Serialization;

namespace TastyTrails.Infrastructure.Integrations.Wolt.DTOs
{
    public record WoltOrderMerchantInfoDto
    {
        [JsonPropertyName("origin_id")]
        public int OriginId { get; init; }
    }
}
