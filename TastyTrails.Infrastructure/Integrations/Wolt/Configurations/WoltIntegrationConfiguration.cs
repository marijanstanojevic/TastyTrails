using System.ComponentModel.DataAnnotations;
using TastyTrails.Common.Attributes.Validation;

namespace TastyTrails.Infrastructure.Integrations.Wolt.Configurations
{
    public class WoltIntegrationConfiguration
    {
        [NotNullOrWhiteSpace, Url]
        public string BaseUrl { get; set; }

        [NotNullOrWhiteSpace]
        public string ApiKey { get; set; }
    }
}
