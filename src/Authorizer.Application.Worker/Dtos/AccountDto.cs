using Newtonsoft.Json;

namespace Authorizer.Application.Worker.Dtos
{
    public struct AccountDto
    {
        //[JsonProperty("activeCard")]
        public bool ActiveCard { get; set; }

        //[JsonProperty("availableLimit")]
        public int AvailableLimit { get; set; }
    }

    public struct RootAccountDto
    {
        public AccountDto account { get; set; }
    }
}
