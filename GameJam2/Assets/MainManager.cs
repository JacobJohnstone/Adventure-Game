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
    [System.NonSerialized] public int level = 1;

    [System.NonSerialized] public bool canMove = true;

    [System.NonSerialized] public int fireballDamage = 20;
    [System.NonSerialized] public int fireballManaCost = 4;
    [System.NonSerialized] public int poisonDamage = 3; // 3
    [System.NonSerialized] public int poisonManaCost = 2;
    [System.NonSerialized] public int shockDamage = 4;
    [System.NonSerialized] public int shockManaCost = 1;

    [System.NonSerialized] public int slashDamage = 7;
    [System.NonSerialized] public int pierceDamage = 2;
    [System.NonSerialized] public int deflectDamage = 5;
    [System.NonSerialized] public int bashDamage = 2;

    [System.NonSerialized] public int missChance = 15; // 15
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

    public void levelUp ()
    {
        // switch statment,
        // on each level, adjust damages for abilities, lower miss chance, increase poison duration
        switch (level) {
            case 2:
                {
                    Debug.Log("Into case 2 for leveling up");
                    missChance = 13; // 13
                    poisonDamage = 5; // 5
                    shockDamage = 8;
                    bashDamage = 9;
                    maxMana = 6;
                    maxHP = 40;
                    currentHP = currentHP + 20;
                    break;
                }
            case 3: {
                    Debug.Log("Into case 3 for leveling up");
                    missChance = 10; //10
                    poisonDamage = 8; // 8
                    shockDamage = 12;
                    slashDamage = 18;
                    bashDamage = 12;
                    maxMana = 8;
                    maxHP = 65;
                    currentHP = currentHP + 25;
                    break;
                }
        
        }

    }

}