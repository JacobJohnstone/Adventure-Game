public class Attack
{
    int damage;
    AttackTypes type;

    public Attack(int damage, AttackTypes type)
    {
        this.damage = damage;
        this.type = type;
    }

    public int getDamage()
    {
        return damage;
    }

    public AttackTypes getType()
    {
        return type;
    }
}
