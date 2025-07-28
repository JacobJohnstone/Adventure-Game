using System.Collections.Generic;
using UnityEngine;

/*
    - Ice Blast
    - etc.
 */

public enum PlayerAttacks { FIREBALL, POISON, SHOCK, SLASH, PIERCE, DEFLECT, BASH }

public class PlayerUnit : Unit
{

    [System.NonSerialized] public int unitLevel;
    [System.NonSerialized] public int maxMana;
    [System.NonSerialized] public int currentMana;

    public Dictionary<PlayerAttacks, Attack> attackMap = new Dictionary<PlayerAttacks, Attack>();

    private void Awake()
    {
        maxHP = MainManager.instance.maxHP;
        currentHP = MainManager.instance.currentHP;
        maxMana = MainManager.instance.maxMana;
        currentMana = maxMana;

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
        attackMap.Add(PlayerAttacks.FIREBALL, new Attack(5, AttackTypes.FIRE));
        attackMap.Add(PlayerAttacks.POISON, new Attack(3, AttackTypes.POISON));
        attackMap.Add(PlayerAttacks.SHOCK, new Attack(4, AttackTypes.ELECTRIC));
        attackMap.Add(PlayerAttacks.SLASH, new Attack(7, AttackTypes.NORMAL));
        attackMap.Add(PlayerAttacks.PIERCE, new Attack(2, AttackTypes.PIERCING));
        attackMap.Add(PlayerAttacks.DEFLECT, new Attack(0, AttackTypes.NORMAL));
        attackMap.Add(PlayerAttacks.BASH, new Attack(5, AttackTypes.FIRE));
    }
}
