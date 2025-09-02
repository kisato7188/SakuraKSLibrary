namespace SKSLib.RPG;

public class Status
{
    public int Hp { get; set; }
    public int Mp { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }

    public Status(int hp, int mp, int attack, int defense)
    {
        Hp = hp;
        Mp = mp;
        Attack = attack;
        Defense = defense;
    }
}
