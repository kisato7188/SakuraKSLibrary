namespace SKSLib.RPG.Shop;

/// <summary>
/// Provides methods for handling transactions between a player and a shop.
/// </summary>
public class TransactionService
{
    private readonly Shop _shop;

    public TransactionService(Shop shop)
    {
        _shop = shop ?? throw new ArgumentNullException(nameof(shop));
    }

    /// <summary>
    /// Attempts to purchase an item from the shop.
    /// </summary>
    /// <param name="item">The item to purchase.</param>
    /// <param name="quantity">The quantity to purchase.</param>
    /// <param name="playerInventory">The player's inventory.</param>
    /// <param name="playerMoney">The player's current money.</param>
    /// <param name="shopMoney">The shop's current money.</param>
    /// <returns>A <see cref="TransactionResult"/> describing the outcome.</returns>
    public TransactionResult Purchase(Item item, int quantity, Inventory playerInventory, Money playerMoney, Money shopMoney)
    {
        if (!_shop.CanPurchase(item, quantity, playerMoney))
        {
            return new TransactionResult(false, "Cannot purchase item.", playerMoney, shopMoney);
        }

        try
        {
            var updatedPlayerMoney = _shop.Purchase(item, quantity, playerInventory, playerMoney);
            var totalCost = checked(_shop.PriceList[item].Amount * quantity);
            var updatedShopMoney = shopMoney.Add(totalCost);
            return new TransactionResult(true, "Purchase successful.", updatedPlayerMoney, updatedShopMoney);
        }
        catch (Exception ex)
        {
            return new TransactionResult(false, ex.Message, playerMoney, shopMoney);
        }
    }

    /// <summary>
    /// Attempts to sell an item to the shop.
    /// </summary>
    /// <param name="item">The item to sell.</param>
    /// <param name="quantity">The quantity to sell.</param>
    /// <param name="playerInventory">The player's inventory.</param>
    /// <param name="playerMoney">The player's current money.</param>
    /// <param name="shopMoney">The shop's current money.</param>
    /// <returns>A <see cref="TransactionResult"/> describing the outcome.</returns>
    public TransactionResult Sell(Item item, int quantity, Inventory playerInventory, Money playerMoney, Money shopMoney)
    {
        if (!_shop.CanSell(item, quantity, playerInventory, playerMoney))
        {
            return new TransactionResult(false, "Cannot sell item.", playerMoney, shopMoney);
        }

        var totalValue = checked(_shop.PriceList[item].Amount * quantity);
        if (shopMoney.Amount < totalValue)
        {
            return new TransactionResult(false, "Shop does not have enough money.", playerMoney, shopMoney);
        }

        try
        {
            var updatedPlayerMoney = _shop.Sell(item, quantity, playerInventory, playerMoney);
            var updatedShopMoney = shopMoney.Subtract(totalValue);
            return new TransactionResult(true, "Sale successful.", updatedPlayerMoney, updatedShopMoney);
        }
        catch (Exception ex)
        {
            return new TransactionResult(false, ex.Message, playerMoney, shopMoney);
        }
    }
}

/// <summary>
/// Represents the result of a transaction between a player and a shop.
/// </summary>
public class TransactionResult
{
    public bool Success { get; }
    public string Message { get; }
    public Money PlayerMoney { get; }
    public Money ShopMoney { get; }

    public TransactionResult(bool success, string message, Money playerMoney, Money shopMoney)
    {
        Success = success;
        Message = message;
        PlayerMoney = playerMoney;
        ShopMoney = shopMoney;
    }
}

