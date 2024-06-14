namespace Order.Domain.VOs;

public class PaymentInfo(string cardNumber, string fullName, string expiration, string cvv)
{
	public string CardNumber { get; } = cardNumber;
	public string FullName { get; } = fullName;
	public string Expiration { get; } = expiration;
	public string Cvv { get; } = cvv;

	public override bool Equals(object? obj)
	{
		return obj is PaymentInfo info &&
		       CardNumber == info.CardNumber &&
		       FullName == info.FullName &&
		       Expiration == info.Expiration &&
		       Cvv == info.Cvv;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(CardNumber, FullName, Expiration, Cvv);
	}
}