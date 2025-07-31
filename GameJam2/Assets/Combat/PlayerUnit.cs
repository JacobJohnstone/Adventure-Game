using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerClasses { MAGE, FIGHTER, TANK }
public enum PlayerAttacks { FIREBALL, POISON, SHOCK, SLASH, PIERCE, DEFLECT, BASH }



public class PlayerUnit : Unit
{
    [Header("Sprite Class Options")]
    public Sprite mage;
    public Sprite fighter;
    public Sprite tank;

    public SpriteRenderer spriteRenderer;

    public PlayerHud playerHud;

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

        if (MainManager.instance.level > 4)
        {
            unitLevel = 5;
        }
        else if (MainManager.instance.level > 3)
        {
            unitLevel = 4;
        }
        else if (MainManager.instance.level > 2)
        {
            unitLevel = 3;
        }
        else if (MainManager.instance.level > 1)
        {
            unitLevel = 2;
        }
        else
        {
            unitLevel = 1;
        }

        MainManager.instance.currentMana = maxMana;

        GameObject playerHudGO = GameObject.FindGameObjectWithTag("PlayerHud");
        if (playerHudGO != null)
        {
            playerHud = playerHudGO.GetComponent<PlayerHud>();
            playerHud.playerClass = currentClass;
        }

        //FIREBALL, SPLASH, SHOCK, SLASH, PIERCE, DEFLECT, BASH
        attackMap.Add(PlayerAttacks.FIREBALL, new Attack(MainManager.instance.fireballDamage, AttackTypes.FIRE, MainManager.instance.fireballManaCost));
        attackMap.Add(PlayerAttacks.POISON, new Attack(MainManager.instance.poisonDamage, AttackTypes.POISON, MainManager.instance.poisonManaCost));
        attackMap.Add(PlayerAttacks.SHOCK, new Attack(MainManager.instance.shockDamage, AttackTypes.ELECTRIC, MainManager.instance.shockManaCost));
        attackMap.Add(PlayerAttacks.SLASH, new Attack(MainManager.instance.slashDamage, AttackTypes.NORMAL));
        attackMap.Add(PlayerAttacks.PIERCE, new Attack(MainManager.instance.pierceDamage, AttackTypes.PIERCING));
        attackMap.Add(PlayerAttacks.DEFLECT, new Attack(MainManager.instance.deflectDamage, AttackTypes.NORMAL));
        attackMap.Add(PlayerAttacks.BASH, new Attack(MainManager.instance.bashDamage, AttackTypes.FIRE));
    }

    public void SetClass(string playerClass)
    {
        switch (playerClass)
        {
            case "mage":
                {
                    spriteRenderer.sprite = mage;
                    currentClass = PlayerClasses.MAGE;
                    break;
                }
            case "tank":
                {
                    Debug.Log("Now a tank");
                    spriteRenderer.sprite = tank;
                    currentClass = PlayerClasses.TANK;
                    break;
                }
            default:
                {
                    spriteRenderer.sprite = fighter;
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

        MainManager.instance.currentMana = currentMana;

        return currentMana;

    }

    public int ManaRegen(int regenAmount)
    {
        currentMana += regenAmount;

        if (currentMana >= maxMana)
        {
            currentMana = maxMana;
        }

        MainManager.instance.currentMana = currentMana;


        return currentMana;

    }
}
