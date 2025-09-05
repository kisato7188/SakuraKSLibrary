namespace SKSLib.RPG.Shop;

public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

public enum ItemCategory
{
    Consumable,
    Equipment,
    Material,
    Quest
}

public class Item
{
    public int Id { get; }
    public string Name { get; }
    public string? Description { get; }
    public int BasePrice { get; }
    public ItemRarity Rarity { get; }
    public ItemCategory Category { get; }

    public Item(int id, string name, int basePrice, ItemRarity rarity, ItemCategory category, string? description = null)
    {
        Id = id;
        Name = name;
        BasePrice = basePrice;
        Rarity = rarity;
        Category = category;
        Description = description;
    }
}
