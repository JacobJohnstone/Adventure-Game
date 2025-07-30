public class Attack
{
    int damage;
    int manaCost;
    AttackTypes type;

    public Attack(int damage, AttackTypes type)
    {
        this.damage = damage;
        this.type = type;
        this.manaCost = 0;
    }

    public Attack(int damage, AttackTypes type, int manaCost)
    {
        this.damage = damage;
        this.type = type;
        this.manaCost = manaCost;
    }

    public int getDamage()
    {
        return damage;
    }

    public AttackTypes getType()
    {
        return type;
    }

    public int getManaCost()
    {
        return manaCost;
    }
}
