using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    Transform nextSpawn;

    // Player stats
    [System.NonSerialized] public int maxHP = 10;
    [System.NonSerialized] public int currentHP = 6;
    [System.NonSerialized] public int maxMana = 20; // regain 10 per turn
    [System.NonSerialized] public int xp = 0;
    [System.NonSerialized] public int damage = 5;

    // Extra potential uses
    public bool foundSpellBook;

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
}
