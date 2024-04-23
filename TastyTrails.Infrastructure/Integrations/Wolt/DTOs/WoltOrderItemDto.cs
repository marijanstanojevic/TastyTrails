using System.Text.Json.Serialization;

namespace TastyTrails.Infrastructure.Integrations.Wolt.DTOs
{
    public record WoltOrderItemDto
    {
        [JsonPropertyName("amount")]
        public int Amount { get; init; }

        [JsonPropertyName("merchant_custom_data")]
        public WoltOrderMerchantInfoDto MerchantInfo { get; init; }
    }
}
