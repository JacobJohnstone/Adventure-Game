using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    Transform nextSpawn;

    // Player stats
    [System.NonSerialized] public int maxHP = 20;
    [System.NonSerialized] public int currentHP = 20;
    [System.NonSerialized] public int maxMana = 20; // regain 10 per turn
    [System.NonSerialized] public int level = 3;

    [System.NonSerialized] public bool canMove = true;

    [System.NonSerialized] public int fireballDamage = 5;
    [System.NonSerialized] public int fireballManaCost = 3;
    [System.NonSerialized] public int poisonDamage = 3;
    [System.NonSerialized] public int poisonManaCost = 1;
    [System.NonSerialized] public int shockDamage = 4;
    [System.NonSerialized] public int shockManaCost = 2;

    [System.NonSerialized] public int slashDamage = 7;
    [System.NonSerialized] public int pierceDamage = 2;
    [System.NonSerialized] public int deflectDamage = 5;
    [System.NonSerialized] public int bashDamage = 2;

    [System.NonSerialized] public int missChance = 15; // out of 100 (20 = 20% miss chance)
    [System.NonSerialized] public int poisonDuration = 2;

    // Extra potential uses
    public bool foundSpellBook = false;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void levelUp (int level)
    {
        // switch statment,
        // on each level, adjust damages for abilities, lower miss chance, increase poison duration
        switch (level) {
            case 2:
                {
                    missChance = 10;
                    break;
                }
            case 3: { 
                    missChance = 5;
                    break;
                }
        
        }

    }

}


//attackMap.Add(PlayerAttacks.FIREBALL, new Attack(5, AttackTypes.FIRE, 3));
//attackMap.Add(PlayerAttacks.POISON, new Attack(3, AttackTypes.POISON, 1));
//attackMap.Add(PlayerAttacks.SHOCK, new Attack(4, AttackTypes.ELECTRIC, 2));
//attackMap.Add(PlayerAttacks.SLASH, new Attack(7, AttackTypes.NORMAL));
//attackMap.Add(PlayerAttacks.PIERCE, new Attack(2, AttackTypes.PIERCING));
//attackMap.Add(PlayerAttacks.DEFLECT, new Attack(0, AttackTypes.NORMAL));
//attackMap.Add(PlayerAttacks.BASH, new Attack(2, AttackTypes.FIRE));