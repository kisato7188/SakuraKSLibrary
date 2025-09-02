namespace SKSLib.RPG;

public class Battler
{
    public int Id { get; }
    public string Name { get; }
    public Status Status { get; }
    public IList<Skill> Skills { get; }

    public Battler(int id, string name, Status status, IEnumerable<Skill>? skills = null)
    {
        Id = id;
        Name = name;
        Status = status;
        Skills = skills?.ToList() ?? new List<Skill>();
    }
}
