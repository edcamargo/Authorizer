using Newtonsoft.Json;
using System;

namespace Authorizer.Domain.Dtos.Transation
{
    public struct TransactionDto
    {
        [JsonProperty(PropertyName = "merchant")]
        public string Merchant { get; private set; }

        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; private set; }

        [JsonProperty(PropertyName = "time")]
        public DateTime Time { get; private set; }
    }

    public struct RootTransactionDto
    {
        public TransactionDto transaction { get; set; }
    }
}
