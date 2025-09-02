namespace SKSLib.RPG;

public class Skill
{
    public int Id { get; }
    public string Name { get; }
    public string? Description { get; }

    public Skill(int id, string name, string? description = null)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}
