namespace SKSLib.RPG.Shop;

public class Shop
{
    public Inventory Inventory { get; }
    private readonly Dictionary<Item, Money> _priceList = new();

    public IReadOnlyDictionary<Item, Money> PriceList => _priceList;

    public Shop(Inventory inventory, IDictionary<Item, Money>? priceList = null)
    {
        Inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        if (priceList != null)
        {
            foreach (var pair in priceList)
            {
                _priceList[pair.Key] = pair.Value;
            }
        }
    }

    public bool CanPurchase(Item item, int quantity, Money funds)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");
        }
        if (!_priceList.TryGetValue(item, out var price))
        {
            return false;
        }
        if (funds.Currency != price.Currency)
        {
            return false;
        }
        var totalCost = checked(price.Amount * quantity);
        return Inventory.GetQuantity(item) >= quantity && funds.Amount >= totalCost;
    }

    public Money Purchase(Item item, int quantity, Inventory buyerInventory, Money funds)
    {
        if (!CanPurchase(item, quantity, funds))
        {
            throw new InvalidOperationException("Cannot purchase item.");
        }

        Inventory.RemoveItem(item, quantity);
        buyerInventory.AddItem(item, quantity);
        var totalCost = checked(_priceList[item].Amount * quantity);
        return funds.Subtract(totalCost);
    }

    public bool CanSell(Item item, int quantity, Inventory sellerInventory, Money funds)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");
        }
        if (!_priceList.TryGetValue(item, out var price))
        {
            return false;
        }
        if (funds.Currency != price.Currency)
        {
            return false;
        }
        return sellerInventory.GetQuantity(item) >= quantity;
    }

    public Money Sell(Item item, int quantity, Inventory sellerInventory, Money funds)
    {
        if (!CanSell(item, quantity, sellerInventory, funds))
        {
            throw new InvalidOperationException("Cannot sell item.");
        }

        sellerInventory.RemoveItem(item, quantity);
        Inventory.AddItem(item, quantity);
        var totalValue = checked(_priceList[item].Amount * quantity);
        return funds.Add(totalValue);
    }
}

