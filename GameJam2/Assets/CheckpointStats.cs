using UnityEngine;

public class CheckpointStats : MonoBehaviour
{
    public static CheckpointStats instance;

    // Player stats
    public int maxHP;
    public int currentHP;
    public int level;

    // Extra potential uses
    public bool foundSpellBook;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
