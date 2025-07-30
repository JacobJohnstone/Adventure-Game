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
    GameObject fallbackText;

    //Text nameText;
    //Text levelText;

    [SerializeField]
    Slider hpSlider;
    [SerializeField]
    Slider manaSlider;
    [SerializeField]
    Text hpText;
    [SerializeField]
    Text manaText;

    void Start()
    {
        hpSlider.maxValue = 1;
        manaSlider.maxValue = 1;
    }

    public void SetHUD(PlayerUnit unit)
    {
        //nameText.text = unit.unitName;
        //levelText.text = "Level " + unit.unitLevel;
        hpSlider.value = (float)unit.currentHP/unit.maxHP;
        hpText.text = "HP " + unit.currentHP + "/" + unit.maxHP;
        manaSlider.value = (float)unit.currentMana/unit.maxMana;
        manaText.text = "Mana " + unit.currentMana + "/" + unit.maxMana;
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
        classOptions.SetActive(false);
        mageOptions.SetActive(true);
    }

    public void OnFighterButton()
    {
        classOptions.SetActive(false);
        fighterOptions.SetActive(true);
    }

    public void OnTankButton()
    {
        classOptions.SetActive(false);
        tankOptions.SetActive(true);
    }

    public void ShowTargetOptions()
    {
        tankOptions.SetActive(false);
        fighterOptions.SetActive(false);
        mageOptions.SetActive(false);
        targetOptions.SetActive(true);
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
            fallbackText.SetActive(true);
        } else
        {
            classOptions.SetActive(true);
            fallbackText.SetActive(false);
        }
    }
    #endregion 


}
