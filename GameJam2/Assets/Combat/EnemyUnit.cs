using UnityEngine;
using System;
using NUnit.Framework;
using System.Collections.Generic;

public class EnemyUnit : Unit
{
    public AttackTypes[] weaknesses;
    public AttackTypes[] resistances;
    public new string unitName;
    public new int maxHP;
    public new int currentHP;
    public bool isSmart = false;
    public bool isPoisoned = false;
    public int poisonedRoundsLeft = 0;

    [HideInInspector]
    public EnemyAttackObj queuedAttack = null;

    int lastAttackIndex = -1;

    [SerializeField]
    public List<EnemyAttackObj> attackList = new List<EnemyAttackObj>();

    private void Awake()
    {
        queuedAttack = null;
    }

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

    public EnemyAttackObj PickAttack()
    {
        EnemyAttackObj attack;
        
        if(lastAttackIndex < 0)
        {
            attack = attackList[lastAttackIndex + 1];
        }
        else
        {
            attack = attackList[lastAttackIndex];
        }

        if (isSmart)
        {

        } 
        else
        {
            Debug.Log("Picking attack because dumb");
            // iterate through attack list
            if (lastAttackIndex != attackList.Count - 1) { 
                
                // set attack
                attack = attackList[lastAttackIndex + 1];
                // increment attack index to the current attack
                lastAttackIndex++;
            } 
            else
            {
                // We've reached the end of the attack list
                attack = attackList[0];
                lastAttackIndex = 0;
            }

        }

        return attack;
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
