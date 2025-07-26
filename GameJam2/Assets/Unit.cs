using UnityEngine;

public class Unit : MonoBehaviour
{

    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;


    int healEffect = 5;

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
