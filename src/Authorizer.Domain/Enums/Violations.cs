namespace Authorizer.Domain.Enums
{
    public enum Violations
    {
        ACCOUNT_ALREADY_INITIALIZED,
	    INSUFFICIENT_LIMIT,
	    CARD_NOT_ACTIVE,
	    HIGH_FREQUENCY_SMALL_INTERVAL,
	    DOUBLED_TRANSACTION
    }
}
