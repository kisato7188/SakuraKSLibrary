namespace SKSLib.RPG.Shop;

/// <summary>
/// Provides functionality to calculate the final price of an item.
/// Price modifiers such as discounts or taxes can be supplied via <see cref="IPriceModifier"/> implementations.
/// </summary>
public class PricingService
{
    private readonly IEnumerable<IPriceModifier> _modifiers;

    /// <summary>
    /// Initializes a new instance of the <see cref="PricingService"/> class.
    /// </summary>
    /// <param name="modifiers">A collection of price modifiers to apply. Can be empty.</param>
    public PricingService(IEnumerable<IPriceModifier>? modifiers = null)
    {
        _modifiers = modifiers ?? Array.Empty<IPriceModifier>();
    }

    /// <summary>
    /// Calculates the final price for the specified item and base price.
    /// </summary>
    /// <param name="item">The item being priced.</param>
    /// <param name="basePrice">The base price of the item.</param>
    /// <returns>The price after all modifiers have been applied.</returns>
    public Money GetFinalPrice(Item item, Money basePrice)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }
        if (basePrice == null)
        {
            throw new ArgumentNullException(nameof(basePrice));
        }

        var price = basePrice;
        foreach (var modifier in _modifiers)
        {
            price = modifier.Apply(item, price);
        }

        return price;
    }
}

/// <summary>
/// Represents a component that can modify an item's price.
/// Implement this interface to add custom pricing logic such as discounts or taxes.
/// </summary>
public interface IPriceModifier
{
    /// <summary>
    /// Applies a price modification.
    /// </summary>
    /// <param name="item">The item being priced.</param>
    /// <param name="currentPrice">The current price before modification.</param>
    /// <returns>The modified price.</returns>
    Money Apply(Item item, Money currentPrice);
}

