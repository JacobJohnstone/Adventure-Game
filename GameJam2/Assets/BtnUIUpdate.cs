using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BtnUIUpdate : MonoBehaviour
{
    public PlayerAttacks attack;
    public Text damageText;
    public Text manaText;

    private void Start()
    {
        switch (attack)
        {
            case PlayerAttacks.FIREBALL:
                {
                    UpdateActionsText(MainManager.instance.fireballDamage, MainManager.instance.fireballManaCost);
                    break;
                }
            case PlayerAttacks.POISON:
                {
                    UpdateActionsText(MainManager.instance.poisonDamage, MainManager.instance.poisonManaCost);
                    break;
                }
            case PlayerAttacks.SHOCK:
                {
                    UpdateActionsText(MainManager.instance.shockDamage, MainManager.instance.shockManaCost);
                    break;
                }
            case PlayerAttacks.SLASH:
                {
                    UpdateActionsText(MainManager.instance.slashDamage);
                    break;
                }
            case PlayerAttacks.PIERCE: {
                    UpdateActionsText(MainManager.instance.pierceDamage);
                    break;
                }
            case PlayerAttacks.DEFLECT:
                {
                    UpdateActionsText(MainManager.instance.deflectDamage);
                    break;
                }
            case PlayerAttacks.BASH: {
                    UpdateActionsText(MainManager.instance.bashDamage);
                    break;
                }
        }
    }

    public void UpdateActionsText(int damage)
    {
        damageText.text = damage + " Damage";
    }

    public void UpdateActionsText(int damage, int mana)
    {
        damageText.text = damage + " Damage";
        manaText.text =  mana + " Mana";
    }

}
