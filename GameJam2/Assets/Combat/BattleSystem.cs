using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    private BattleState state;
    private float dialoguePause = 3f;

    /*
        Make sure to set all public variable values in the editor, 
        or make them private if you're taking them from the scriptable object
     */
    public GameObject playerCombatPrefab;
    public List<GameObject> enemyCombatPrefabs;

    public Transform playerArea;
    public List<Transform> enemyAreas;

    public Text dialogueText;

    PlayerUnit playerUnit;
    List<EnemyUnit> enemyUnits = new List<EnemyUnit>();
    
    public PlayerHud playerHUD;
    List<EnemyHud> enemyHuds = new List<EnemyHud>();
    int activeEnemyIndex = 0;


    PlayerAttacks pendingAttack;


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

        for (int i = 0; i < enemyCombatPrefabs.Count; i++)
        {
            GameObject enemyGO = Instantiate(enemyCombatPrefabs[i], enemyAreas[i]);
            EnemyUnit newEnemyUnit = enemyGO.GetComponent<EnemyUnit>();

            // If there is something off about the indexes, replace these add function with .insert(index, gameObject)
            enemyUnits.Add(newEnemyUnit);
            EnemyHud enemyHud = enemyGO.GetComponentInChildren<EnemyHud>();
            enemyHud.SetHUD(newEnemyUnit);
            enemyHuds.Add(enemyHud);
        }

        // Drag the canvas text element you want to display the name of the enemy
        dialogueText.text = "A group of enemies has approached!";
        playerHUD.SetHUD(playerUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "Your turn!"; //playerUnit.name + ", your turn!";
        // mana regen each combat round
        int mana = playerUnit.ManaRegen(1);
        playerHUD.SetMana(playerUnit.currentMana, playerUnit.maxMana);
        // disable block if it's still up from last turn
        playerUnit.isBlocking = false;
    }

    public void OnAttackButton(string attackStr)
    {
        if (state != BattleState.PLAYERTURN)
            return;

        if(Enum.TryParse(attackStr, true, out PlayerAttacks attack))
        {
            Debug.Log(attackStr + " pending: " + attack);
            pendingAttack = attack;
            playerHUD.ShowTargetOptions();
        } 
        else
        {
            Debug.LogWarning("Invalid attack type: " + attackStr);
        }
    }

    public void OnTargetSelection(int target) 
    {
        playerHUD.OnStateUpdate(BattleState.ENEMYTURN);
        StartCoroutine(PlayerAttack(pendingAttack, target));
    }

    public void OnBlockButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        playerHUD.OnStateUpdate(BattleState.ENEMYTURN);
        StartCoroutine(PlayerBlock());

    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        playerHUD.OnStateUpdate(BattleState.ENEMYTURN);
        StartCoroutine(PlayerHeal());
    }

    IEnumerator PlayerBlock()
    {
        playerUnit.isBlocking = true;

        dialogueText.text = "You are preparing to block!";
        yield return new WaitForSeconds(dialoguePause);

        state = BattleState.ENEMYTURN;
        playerHUD.OnStateUpdate(state);
        EnemyTurn();
    }

    IEnumerator PlayerAttack(PlayerAttacks attack, int targetIndex)
    {
        // Attack information //

        // Damage value
        int damage = playerUnit.attackMap[attack].getDamage();
        // Elemental effects
        AttackTypes damageType = playerUnit.attackMap[attack].getType();
        // Mana cost (attack that don't use mana have their cost set to 0)
        playerUnit.UseMana(playerUnit.attackMap[attack].getManaCost());
        playerHUD.SetMana(playerUnit.currentMana, playerUnit.maxMana);

        // damage the enemy, TakeDamage() returns a bool depending if the attack kills or not
        EnemyUnit attackTarget = enemyUnits[targetIndex];
        bool isDead = attackTarget.TakeDamage(damage, damageType);

        // update playerHUD
        dialogueText.text = "You used " + attack;
        yield return new WaitForSeconds(dialoguePause);

        // update UI
        //enemyHUD.SetHP((float)enemyUnit.currentHP / enemyUnit.maxHP, enemyUnit.currentHP, enemyUnit.maxHP);
        enemyHuds[targetIndex].SetHP((float)attackTarget.currentHP / attackTarget.maxHP, attackTarget.currentHP, attackTarget.maxHP);


        dialogueText.text = "The attack was successful";
        yield return new WaitForSeconds(dialoguePause);

        // check if the enemy is dead
        if (isDead)
        {
            dialogueText.text = "That was a finishing blow!";
            yield return new WaitForSeconds(dialoguePause);
            Destroy(attackTarget.gameObject);
            enemyUnits.RemoveAt(targetIndex);
            enemyHuds.RemoveAt(targetIndex);
            if (enemyUnits.Count <= 0) {
                // end the battle
                state = BattleState.WON;
                StartCoroutine(EndBattle());
            } else
            {
                dialogueText.text = "It's their turn now!";

                yield return new WaitForSeconds(dialoguePause);
                // enemy turn
                state = BattleState.ENEMYTURN;
                playerHUD.OnStateUpdate(state);
                EnemyTurn();
            }
        } else
        {
            // enemy dialogue change would go here, then a WaitForSeconds or something
            dialogueText.text = "You did " + damage + " damage!";

            yield return new WaitForSeconds(dialoguePause);

            dialogueText.text = "It's their turn now!";

            yield return new WaitForSeconds(dialoguePause);
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

        yield return new WaitForSeconds(dialoguePause);

        playerUnit.Heal();

        // Update enemy HUD -- health bar

        state = BattleState.ENEMYTURN;
        playerHUD.OnStateUpdate(state);
        EnemyTurn();
    }

    void EnemyTurn()
    {
        // Default move, attack..
        StartCoroutine(EnemyAttack());
    }

    IEnumerator EnemyAttack()
    {
        // randomize order of enemy attacks
        int[] indecies = new int[enemyUnits.Count];
        int tempindex = 0;
        while(tempindex < indecies.Length)
        {
            indecies[tempindex++] = tempindex;
        }

        // indecies now contains the index values in order, here we shuffle them, then use forEach to loop through the now random order of units, without casuing out of bounds errors


        for (int i = 0; i < enemyUnits.Count; i++)
        {
            // Safety check in case a dead enemy doesn't get removed from enemeyUnits properly
            if (enemyUnits[i] == null || enemyUnits[i].currentHP <= 0)
            {
                continue;
            }

            // Enemy attack dialogue and pause
            if(i == 0)
            {
                dialogueText.text = "The enemy is attacking!";
            }
            else
            {
                dialogueText.text = "The next enemy is attacking!";
            }
            yield return new WaitForSeconds(dialoguePause);

            bool isDead = false;
            int damage = enemyUnits[i].damage;

            if (!playerUnit.isBlocking) // && this attack can be blocked?
            {
                if (playerUnit.currentClass == PlayerClasses.TANK)
                {
                    Debug.Log("Taking random dmg because you're a tank");
                    int blockedDmg = Random.Range(0, 3); // if the player is on the tank role, block a random amount of damage, lowest block being 0, highest block being 2
                    isDead = playerUnit.TakeDamage(damage - 2);
                    dialogueText.text = "You blocked " + blockedDmg + " damage!";
                    yield return new WaitForSeconds(dialoguePause);
                    dialogueText.text = "The enemy did " + (damage - blockedDmg) + " damage!";
                    yield return new WaitForSeconds(dialoguePause);
                }
                else
                {
                    isDead = playerUnit.TakeDamage(damage);
                    dialogueText.text = "The enemy did " + damage + " damage!";
                    yield return new WaitForSeconds(dialoguePause);
                }
            }
            else
            {
                dialogueText.text = "You blocked " + damage + " damage!";
                yield return new WaitForSeconds(dialoguePause);
            }

            // update playerHUD
            playerHUD.SetHP((float)playerUnit.currentHP / playerUnit.maxHP, playerUnit.currentHP, playerUnit.maxHP);

            if (isDead)
            {
                state = BattleState.LOST;
                StartCoroutine(EndBattle());
                yield break;
            }

        }

        // If we reach this code, the player did not die during all the enemy turns, and we can pass it back to the player turn
        state = BattleState.PLAYERTURN;
        playerHUD.OnStateUpdate(state);
        PlayerTurn();
    }

    IEnumerator EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
            yield return new WaitForSeconds(dialoguePause);
            // update mainmanager XP
            MainManager.instance.xp += 150;
            // update mainmanager hp
            MainManager.instance.currentHP = playerUnit.currentHP;
            // set DoorID to combat won before changing scenes
            SceneSpawns.instance.UpdateDoorID(DoorIDs.wonLevel1Combat);
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated";
            yield return new WaitForSeconds(dialoguePause);

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
