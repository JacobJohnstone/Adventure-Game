using UnityEngine;
using UnityEngine.UI;

/*

    Things to change:
        - Setup player level and name UI elements
        - 

 */

public class PlayerHud : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField] 
    GameObject classOptions;
    [SerializeField] 
    GameObject mageOptions;
    [SerializeField]
    GameObject fighterOptions;
    [SerializeField]
    GameObject tankOptions;
    [SerializeField]
    GameObject targetOptions;
    [SerializeField]
    GameObject target1;
    [SerializeField]
    GameObject target2;
    [SerializeField]
    GameObject target3;
    [SerializeField]
    GameObject fallbackText;
    [SerializeField]
    GameObject fireballContainer;
    [SerializeField]
    GameObject poisonContainer;
    [SerializeField]
    GameObject shockContainer;
    [SerializeField]
    GameObject SlashContainer;

    //Text nameText;
    //Text levelText;
    public PlayerClasses playerClass = PlayerClasses.MAGE;
    int level;
    bool hasSpellBook;

    PlayerUnit playerUnit;

    [SerializeField]
    Slider hpSlider;
    [SerializeField]
    Slider manaSlider;
    [SerializeField]
    Text hpText;
    [SerializeField]
    Text manaText;

    int startingEnemyCount = 3;

    void Start()
    {
        hpSlider.maxValue = 1;
        manaSlider.maxValue = 1;
        hasSpellBook = MainManager.instance.foundSpellBook;

        GameObject playerUnitGO = GameObject.FindGameObjectWithTag("PlayerUnit");
        if (playerUnitGO != null)
        {
            playerUnit = playerUnitGO.GetComponent<PlayerUnit>();
        }
        level = MainManager.instance.level;
    }

    public void SetHUD(PlayerUnit unit, int numOfEnemies)
    {
        //nameText.text = unit.unitName;
        //levelText.text = "Level " + unit.unitLevel;
        hpSlider.value = (float)unit.currentHP/unit.maxHP;
        hpText.text = "HP " + unit.currentHP + "/" + unit.maxHP;
        manaSlider.value = (float)unit.currentMana/unit.maxMana;
        manaText.text = "Mana " + unit.currentMana + "/" + unit.maxMana;
        startingEnemyCount = numOfEnemies;
    }

    public void SetHP(float hp, int currentHP, int maxHP)
    {
        if (hp < 0)
        {
            hp = 0;
            currentHP = 0;
        };
        hpSlider.value = hp;
        hpText.text = "HP " + currentHP + "/" + maxHP;
    }

    public void SetMana(int currentMana, int maxMana)
    {
        manaSlider.value =(float)currentMana/maxMana;
        manaText.text = "Mana " + currentMana + "/" + maxMana;
    }

    // For Class selection + Action selection UI
    #region
    public void OnMageButton()
    {
        playerUnit.SetClass("mage");
        classOptions.SetActive(false);
        mageOptions.SetActive(true);
        if (MainManager.instance.currentMana >= MainManager.instance.shockManaCost)
        {
            shockContainer.SetActive(true);
        }
        else
        {
            shockContainer.SetActive(false);
        }

        if (hasSpellBook && MainManager.instance.currentMana >= MainManager.instance.fireballManaCost)
        {
            fireballContainer.SetActive(true);
        } else
        {
            fireballContainer.SetActive(false);
        }

        if(level > 1 && MainManager.instance.currentMana >= MainManager.instance.poisonManaCost)
        {
            poisonContainer.SetActive(true);
        } else
        {
            poisonContainer.SetActive(false);
        }
    }

    public void OnFighterButton()
    {
        playerUnit.SetClass("fighter");
        classOptions.SetActive(false);
        fighterOptions.SetActive(true);
        if(level > 3)
        {
            SlashContainer.SetActive(true);
        } else
        {
            SlashContainer.SetActive(false);
        }
    }

    public void OnTankButton()
    {
        playerUnit.SetClass("tank");
        classOptions.SetActive(false);
        tankOptions.SetActive(true);
    }

    public void ShowTargetOptions(int targetsLeft)
    {
        //fireballContainer.SetActive(false);
        //poisonContainer.SetActive(false);
        tankOptions.SetActive(false);
        fighterOptions.SetActive(false);
        mageOptions.SetActive(false);
        targetOptions.SetActive(true);
        SetTargetAmount(targetsLeft);
    }

    public void SetTargetAmount(int targetsLeft)
    {

        switch (targetsLeft)
        {
            case 1:
                {
                    target1.SetActive(true);
                    target2.SetActive(false);
                    target3.SetActive(false);
                    break;
                }
            case 2:
                {
                    target1.SetActive(true);
                    target2.SetActive(true);
                    target3.SetActive(false);
                    break;
                }
            case 3:
                {
                    target1.SetActive(true);
                    target2.SetActive(true);
                    target3.SetActive(true);
                    break;
                }
        }
    }

    public void OnStateUpdate(BattleState state)
    {
        if(state != BattleState.PLAYERTURN)
        {
            classOptions.SetActive(false);
            tankOptions.SetActive(false);
            fighterOptions.SetActive(false);
            mageOptions.SetActive(false);
            targetOptions.SetActive(false);
            target1.SetActive(false);
            target2.SetActive(false);
            target3.SetActive(false);
            fallbackText.SetActive(true);
        } else
        {
            classOptions.SetActive(true);
            fallbackText.SetActive(false);
        }
    }
    #endregion 


}
