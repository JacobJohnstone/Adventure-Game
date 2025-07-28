using UnityEngine;
using System;


public class EnemyUnit : Unit
{

    public AttackTypes[] weaknesses;
    public AttackTypes[] resistances;
    public int damage;
    public new string unitName;
    public new int maxHP;
    public new int currentHP;

    // Overload considering attacks that might be strong or weak against a specific unit
    public bool TakeDamage(int damage, AttackTypes typeEffect)
    {
        int totalDamage = damage;

        if (hasEffect(weaknesses, typeEffect)) // If unit is weak to this effect
        {
            totalDamage = damage * 2;
        }
        else if (hasEffect(resistances, typeEffect)) // If unit is resistant to this effect
        {
            // Need casing in the case of damage being an odd number and dealing with integers (e.g., 3/2 returns 1, not 1.5)
            totalDamage = (int)Math.Floor((float)damage / 2);
        }

        currentHP -= totalDamage;

        if (currentHP <= 0)
            return true;

        return false;
    }

    // Util functions
    #region
    bool hasEffect(AttackTypes[] array, AttackTypes effect)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == effect)
            {
                return true;
            }
        }
        return false;
    }


    #endregion


}
