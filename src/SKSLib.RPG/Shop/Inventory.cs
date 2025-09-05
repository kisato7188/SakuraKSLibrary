namespace SKSLib.RPG.Shop;

public class Inventory
{
    private readonly Dictionary<Item, int> _items = new();

    public IReadOnlyDictionary<Item, int> Items => _items;

    public void AddItem(Item item, int quantity = 1)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");
        }

        if (_items.TryGetValue(item, out var current))
        {
            _items[item] = checked(current + quantity);
        }
        else
        {
            _items[item] = quantity;
        }
    }

    public void RemoveItem(Item item, int quantity = 1)
    {
        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");
        }

        if (!_items.TryGetValue(item, out var current) || current < quantity)
        {
            throw new InvalidOperationException("Not enough items to remove.");
        }

        var remaining = current - quantity;
        if (remaining > 0)
        {
            _items[item] = remaining;
        }
        else
        {
            _items.Remove(item);
        }
    }

    public int GetQuantity(Item item)
    {
        return _items.TryGetValue(item, out var quantity) ? quantity : 0;
    }
}
