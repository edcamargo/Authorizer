using Newtonsoft.Json;

namespace Authorizer.Domain.Dtos.Account
{
    public struct AccountDto
    {
        [JsonProperty(PropertyName = "active-card")]
        public bool ActiveCard { get; set; }

        [JsonProperty(PropertyName = "available-limit")]
        public int AvailableLimit { get; set; }
    }

    public struct RootAccountDto
    {
        public AccountDto account { get; set; }
    }
}
