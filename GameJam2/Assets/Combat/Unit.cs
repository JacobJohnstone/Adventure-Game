using System;
using UnityEngine;

public enum AttackTypes { NORMAL, FIRE, ELECTRIC, POISON, PIERCING, SLASHING }

public class Unit : MonoBehaviour
{
    [System.NonSerialized] public string unitName;
    [System.NonSerialized] public int maxHP;
    [System.NonSerialized] public int currentHP;

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
