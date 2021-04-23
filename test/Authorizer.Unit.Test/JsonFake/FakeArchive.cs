namespace Authorizer.Unit.Test.JsonFake
{
    public static class FakeArchive
    {
        public static string AccountInputOne()
        {
            return @"{""account"": {""active-card"": true, ""available-limit"": 100}}";
        }

        public static string AccountInputTwo()
        {
            return @"{""account"": {""active-card"": false, ""available-limit"": 100}}";
        }

        public static string TransactionInputOne()
        {
            return @"{""transaction"": {""merchant"": ""Habbib's"", ""amount"": 90, ""time"": ""2019-02-13T10:02:00.000Z""}}";
        }

        public static string TransactionInputTwo()
        {
            return @"{""transaction"": {""merchant"": ""Habbib's"", ""amount"": 90, ""time"": ""2019-02-13T10:02:00.000Z""}}";
        }
    }
}
