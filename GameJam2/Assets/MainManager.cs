using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;


    Transform nextSpawn;

    // Player stats
    public int maxHP;
    public int currentHP;
     public int xp;

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
