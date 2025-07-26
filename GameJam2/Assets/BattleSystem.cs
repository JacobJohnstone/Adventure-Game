using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
    I am at 13:47. I should watch a tutorial on creating the canvas nice before continuing with this tutorial, and maybe make some art first.
    Proceed tomorrow.
 */

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    private BattleState state;

    /*
        Make sure to set all public variable values in the editor, 
        or make them private if you're taking them from the scriptable object
     */
    public GameObject playerCombatPrefab;
    public GameObject enemyCombatPrefab;

    public Transform playerArea;
    public Transform enemyArea;

    public Text dialogueText;

    Unit playerUnit;
    Unit enemyUnit;
    
    public PlayerHud playerHUD;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        // Still have to create the enemy prefab, probably adjust the player one as well. Don't want to get too far without at least some art
        GameObject playerGO = Instantiate(playerCombatPrefab, playerArea);
        playerUnit = playerGO.GetComponent<Unit>();
        GameObject enemyGO  = Instantiate(enemyCombatPrefab, enemyArea);
        enemyUnit = enemyGO.GetComponent<Unit>();

        // Drag the canvas text element you want to display the name of the enemy
        dialogueText.text = enemyUnit.unitName + " has approached!";
        playerHUD.SetHUD(playerUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = playerUnit.name + ", your turn!";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        
        StartCoroutine (PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

    IEnumerator PlayerAttack()
    {
        // check hit based on accuracy
        // damage the enemy, TakeDamage() returns a bool depending if the attack kills or not
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        // update UI
        //enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack was successful";

        yield return new WaitForSeconds(2f);

        // check if the enemy is dead
        if (isDead)
        {
            // end the battle
            state = BattleState.WON;
            EndBattle();
        } else
        {
            // enemy dialogue change would go here, then a WaitForSeconds or something
            // enemy turn
            state = BattleState.ENEMYTURN;
            EnemyTurn();
        }

        // change state based on what happened
    }

    IEnumerator PlayerHeal()
    {
        dialogueText.text = "You have healed!";

        yield return new WaitForSeconds(2f);

        playerUnit.Heal();

        // Update enemy HUD -- health bar

        state = BattleState.ENEMYTURN;
        EnemyTurn();
    }

    void EnemyTurn()
    {
        // attack, heal, block, etc.?
        if(enemyUnit.currentHP < enemyUnit.maxHP)
        {
            StartCoroutine(EnemyHeal());
        } else if (false)
        {
            // If the player has a charged attack, block
        } else
        {
            // Default move, attack..
            StartCoroutine(EnemyAttack());
        }
        
    }

    IEnumerator EnemyHeal()
    {
        dialogueText.text = enemyUnit.name + " has healed!";

        yield return new WaitForSeconds(2f);

        enemyUnit.Heal();

        // Update enemy HUD -- health bar

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator EnemyAttack()
    {
        bool isDead =  playerUnit.TakeDamage(enemyUnit.damage);

        // update playerHUD
         playerHUD.SetHP(playerUnit.currentHP/playerUnit.maxHP);

        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        } else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
            // update mainmanager XP
            // set DoorID to combat won before changing scenes
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated";
            // set DoorID to level_enter before changing scenes
        }

        // change scenes outside of conditional
    }


}
