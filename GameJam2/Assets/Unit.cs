using UnityEngine;

public class Unit : MonoBehaviour
{

    [System.NonSerialized]  public string unitName = "Player";
    [System.NonSerialized] public int unitLevel;
    [System.NonSerialized] public int damage;
    [System.NonSerialized] public int maxHP;
    [System.NonSerialized] public int currentHP;
    [System.NonSerialized] public int maxMana;
    [System.NonSerialized] public int currentMana;

    int healEffect = 5;

    private void Awake()
    {
        damage = MainManager.instance.damage;
        maxHP = MainManager.instance.maxHP;
        currentHP = MainManager.instance.currentHP;
        maxMana = MainManager.instance.maxMana;
        currentMana = maxMana;

        if (MainManager.instance.xp > 400)
        {
            unitLevel = 5;
        } else if(MainManager.instance.xp > 300)
        {
            unitLevel = 4;
        } else if( MainManager.instance.xp > 200)
        {
            unitLevel = 3;
        }
        else if(MainManager.instance.xp > 100)
        {
             unitLevel = 2;
        } else
        {
            unitLevel = 1;
        }
    }

    public bool TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
            return true;

        return false;
    }

   public void Heal()
    {
        if (currentHP + healEffect > maxHP)
        {
            currentHP = maxHP;
        }
        else {
            currentHP += healEffect;
        }
    }

}
