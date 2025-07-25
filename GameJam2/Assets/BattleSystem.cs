using UnityEngine;
using UnityEngine.UI;

/*
    I am at 13:47. I should watch a tutorial on creating the canvas nice before continuing with this tutorial, and maybe make some art first.
    Proceed tomorrow.
 */

public enum BattleState { START, PALYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    private BattleState state;

    public GameObject playerCombatPrefab;
    public GameObject enemyCombatPrefab;

    public Transform playerArea;
    public Transform enemyArea;

    public Text dialogueText;

    Unit playerUnit;
    Unit enemyUnit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        // Still have to create the enemy prefab, probably adjust the player one as well. Don't want to get too far without at least some art
        GameObject playerGO = Instantiate(playerCombatPrefab, playerArea);
        playerUnit = playerGO.GetComponent<Unit>();
        GameObject enemyGO  = Instantiate(enemyCombatPrefab, enemyArea);
        enemyUnit = enemyGO.GetComponent<Unit>();

        // Drag the canvas text element you want to display the name of the enemy
        dialogueText.text = enemyUnit.unitName;
    }

}
