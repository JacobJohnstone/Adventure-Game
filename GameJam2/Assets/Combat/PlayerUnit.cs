using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerClasses { MAGE, FIGHTER, TANK }
public enum PlayerAttacks { FIREBALL, POISON, SHOCK, SLASH, PIERCE, DEFLECT, BASH }



public class PlayerUnit : Unit
{
    //[Header("Sprite Class Options")]
    //public Sprite mage;
    //public Sprite fighter;
    //public Sprite tank;

    [System.NonSerialized] public int unitLevel;
    [System.NonSerialized] public int maxMana;
    [System.NonSerialized] public int currentMana;
    [System.NonSerialized] public bool isBlocking;
    [System.NonSerialized] public PlayerClasses currentClass;



    public Dictionary<PlayerAttacks, Attack> attackMap = new Dictionary<PlayerAttacks, Attack>();

    private void Awake()
    {
        maxHP = MainManager.instance.maxHP;
        currentHP = MainManager.instance.currentHP;
        maxMana = MainManager.instance.maxMana;
        currentMana = maxMana;
        isBlocking = false;
        currentClass = PlayerClasses.MAGE;

        if (MainManager.instance.xp > 400)
        {
            unitLevel = 5;
        }
        else if (MainManager.instance.xp > 300)
        {
            unitLevel = 4;
        }
        else if (MainManager.instance.xp > 200)
        {
            unitLevel = 3;
        }
        else if (MainManager.instance.xp > 100)
        {
            unitLevel = 2;
        }
        else
        {
            unitLevel = 1;
        }

        //FIREBALL, SPLASH, SHOCK, SLASH, PIERCE, DEFLECT, BASH
        attackMap.Add(PlayerAttacks.FIREBALL, new Attack(MainManager.instance.fireballDamage, AttackTypes.FIRE, MainManager.instance.fireballManaCost));
        attackMap.Add(PlayerAttacks.POISON, new Attack(3, AttackTypes.POISON, 1));
        attackMap.Add(PlayerAttacks.SHOCK, new Attack(4, AttackTypes.ELECTRIC, 2));
        attackMap.Add(PlayerAttacks.SLASH, new Attack(7, AttackTypes.NORMAL));
        attackMap.Add(PlayerAttacks.PIERCE, new Attack(2, AttackTypes.PIERCING));
        attackMap.Add(PlayerAttacks.DEFLECT, new Attack(0, AttackTypes.NORMAL));
        attackMap.Add(PlayerAttacks.BASH, new Attack(2, AttackTypes.FIRE));
    }

    public void SetClass(string playerClass)
    {
        switch (playerClass)
        {
            case "mage":
                {
                    currentClass = PlayerClasses.MAGE;
                    break;
                }
            case "tank":
                {
                    Debug.Log("Now a tank");
                    currentClass = PlayerClasses.TANK;
                    break;
                }
            default:
                {
                    currentClass = PlayerClasses.FIGHTER;
                    break;
                }
        }
    }

    public int UseMana(int manaCost)
    {
        currentMana -= manaCost;

        if (currentMana <= 0)
        {
            currentMana = 0;
        }

        return currentMana;

    }

    public int ManaRegen(int regenAmount)
    {
        currentMana += regenAmount;

        if (currentMana >= maxMana)
        {
            currentMana = maxMana;
        }

        return currentMana;

    }
}
