using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    PlayerUnit playerUnit;
    EnemyUnit enemyUnit;
    
    public PlayerHud playerHUD;
    public EnemyHud enemyHUD;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SceneTransition.instance.TransitionIn());
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        // Still have to create the enemy prefab, probably adjust the player one as well. Don't want to get too far without at least some art
        GameObject playerGO = Instantiate(playerCombatPrefab, playerArea);
        playerUnit = playerGO.GetComponent<PlayerUnit>();
        GameObject enemyGO  = Instantiate(enemyCombatPrefab, enemyArea);
        enemyUnit = enemyGO.GetComponent<EnemyUnit>();

        // Drag the canvas text element you want to display the name of the enemy
        dialogueText.text = enemyUnit.unitName + " has approached!";
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = playerUnit.name + ", your turn!";
    }

    public void OnAttackButton(string attackStr)
    {
        if (state != BattleState.PLAYERTURN)
            return;

        if(Enum.TryParse(attackStr, true, out PlayerAttacks attack))
        {
            Debug.Log(attackStr + " used: " + attack);
            playerHUD.OnStateUpdate(BattleState.ENEMYTURN);
            StartCoroutine(PlayerAttack(attack));
        } 
        else
        {
            Debug.LogWarning("Invalid attack type: " + attackStr);
        }
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        playerHUD.OnStateUpdate(BattleState.ENEMYTURN);
        StartCoroutine(PlayerHeal());
    }

    IEnumerator PlayerAttack(PlayerAttacks attack)
    {
        // damage the enemy, TakeDamage() returns a bool depending if the attack kills or not
        int damage = playerUnit.attackMap[attack].getDamage();
        AttackTypes damageType = playerUnit.attackMap[attack].getType();
        bool isDead = enemyUnit.TakeDamage(damage, damageType);
        // update playerHUD
        enemyHUD.SetHP((float)enemyUnit.currentHP / enemyUnit.maxHP, enemyUnit.currentHP, enemyUnit.maxHP);

        // update UI
        // enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "The attack was successful";

        yield return new WaitForSeconds(2f);

        // check if the enemy is dead
        if (isDead)
        {
            // end the battle
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        } else
        {
            // enemy dialogue change would go here, then a WaitForSeconds or something
            dialogueText.text = "You did " + damage + " damage!";

            yield return new WaitForSeconds(2f);

            dialogueText.text = "It's " + enemyUnit.name + "'s turn!";

            yield return new WaitForSeconds(2f);
            // enemy turn
            state = BattleState.ENEMYTURN;
            playerHUD.OnStateUpdate(state);
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
        playerHUD.OnStateUpdate(state);
        EnemyTurn();
    }

    void EnemyTurn()
    {
        // attack, heal, block, etc.?
        if(false && enemyUnit.currentHP < enemyUnit.maxHP)
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
        playerHUD.OnStateUpdate(state);
        PlayerTurn();
    }

    IEnumerator EnemyAttack()
    {
        bool isDead =  playerUnit.TakeDamage(enemyUnit.damage);

        // update playerHUD
        playerHUD.SetHP((float)playerUnit.currentHP/playerUnit.maxHP, playerUnit.currentHP, playerUnit.maxHP);

        dialogueText.text = "The enemy did " + enemyUnit.damage + " damage!";

        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        } else
        {
            state = BattleState.PLAYERTURN;
            playerHUD.OnStateUpdate(state);
            PlayerTurn();
        }
    }

    IEnumerator EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
            yield return new WaitForSeconds(2f);
            // update mainmanager XP
            // update mainmanager hp
            MainManager.instance.currentHP = playerUnit.currentHP;
            // set DoorID to combat won before changing scenes
            SceneSpawns.instance.UpdateDoorID(DoorIDs.wonLevel1Combat);
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated";
            yield return new WaitForSeconds(2f);

            // set DoorID to level_enter before changing scenes
            SceneSpawns.instance.UpdateDoorID(DoorIDs.enterLevel1);
        }

        // change scenes outside of conditional
        string currentLevel = SceneSpawns.instance.currentLevel.ToString();
        SceneManager.LoadScene(currentLevel, LoadSceneMode.Single);
    }

    void GetDoorSpawn()
    {
        Levels returningLevel = SceneSpawns.instance.currentLevel;

        switch (returningLevel)
        {
            case Levels.level_1: {
                    if(state != BattleState.WON)
                    {
                        SceneSpawns.instance.UpdateDoorID(DoorIDs.enterLevel1);
                    } else
                    {
                        SceneSpawns.instance.UpdateDoorID(DoorIDs.wonLevel1Combat);
                    }
                    break;
                }
            case Levels.level_2:
                {
                    if (state != BattleState.WON)
                    {
                        SceneSpawns.instance.UpdateDoorID(DoorIDs.enterLevel2);
                    }
                    else
                    {
                        SceneSpawns.instance.UpdateDoorID(DoorIDs.wonLevel2Combat);
                    }
                    break;
                }
            case Levels.level_3:
                {
                    //if (state != BattleState.WON)
                    SceneSpawns.instance.UpdateDoorID(DoorIDs.enterLevel3);
                    
                    // else => final scene/cutscene thing for winning game?
                    break;
                }
            default:
                {
                    Debug.Log("[BattleSystem -> GetDoorSpawn()]: This was probably a mistake (default switch statement block).");
                    SceneSpawns.instance.UpdateDoorID(DoorIDs.returnLevel0);
                    break;
                }
        }

    }


}
