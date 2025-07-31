using UnityEngine;

[System.Serializable]
public class EnemyAttackObj
{
    [SerializeField]
    public int damage;
    [SerializeField]
    public bool needsToCharge;
    [SerializeField]
    public int turnRequired; // indicates the number of turns that need to go by before the enemy can use this attack

    public EnemyAttackObj()
    {
        damage = 0;
        needsToCharge = false;
        turnRequired = 0;
    }

    public int GetDamage()
    {
        return damage;
    }

    public bool GetNeedsToCharge()
    {
        return needsToCharge;
    }

    public int GetTurnRequired()
    {
        return turnRequired;
    }
}
