using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    Transform nextSpawn;

    // Player stats
    [System.NonSerialized] public int maxHP = 20;
    [System.NonSerialized] public int currentHP = 20;
    [System.NonSerialized] public int maxMana = 4; // regain 1 per turn
    [System.NonSerialized] public int currentMana = 4;
    [System.NonSerialized] public int level = 3;

    [System.NonSerialized] public bool canMove = true;

    [System.NonSerialized] public int fireballDamage = 15;
    [System.NonSerialized] public int fireballManaCost = 3;
    [System.NonSerialized] public int poisonDamage = 2;
    [System.NonSerialized] public int poisonManaCost = 3;
    [System.NonSerialized] public int shockDamage = 4;
    [System.NonSerialized] public int shockManaCost = 1;

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
                    missChance = 13;
                    poisonDamage = 3;
                    shockDamage = 5;
                    maxMana = 6;
                    maxHP = 40;
                    break;
                }
            case 3: { 
                    missChance = 10;
                    poisonDamage = 4;
                    shockDamage = 6;
                    maxMana = 8;
                    maxHP = 60;
                    break;
                }
        
        }

    }

}