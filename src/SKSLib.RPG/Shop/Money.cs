namespace SKSLib.RPG.Shop;

public class Money
{
    public int Amount { get; }
    public string Currency { get; }

    public Money(int amount, string currency = "G")
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");
        }

        Amount = amount;
        Currency = currency;
    }

    public Money Add(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");
        }

        return new Money(checked(Amount + amount), Currency);
    }

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);
        return Add(other.Amount);
    }

    public Money Subtract(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");
        }

        if (Amount < amount)
        {
            throw new InvalidOperationException("Resulting amount cannot be negative.");
        }

        return new Money(Amount - amount, Currency);
    }

    public Money Subtract(Money other)
    {
        EnsureSameCurrency(other);
        return Subtract(other.Amount);
    }

    public override string ToString() => $"{Amount} {Currency}";

    private void EnsureSameCurrency(Money other)
    {
        if (Currency != other.Currency)
        {
            throw new InvalidOperationException("Currency mismatch.");
        }
    }
}
