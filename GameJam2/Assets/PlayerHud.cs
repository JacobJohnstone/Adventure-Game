using UnityEngine;
using UnityEngine.UI;

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
    GameObject fallbackText;

    //Text nameText;
    //Text levelText;

    [SerializeField]
    Slider hpSlider;
    [SerializeField]
    Text hpText;
    [SerializeField]
    Text manaText;

    void Start()
    {
        hpSlider.maxValue = 1;
    }

    public void SetHUD(Unit unit)
    {
        //nameText.text = unit.unitName;
        //levelText.text = "Level " + unit.unitLevel;
        hpSlider.value = (float)unit.currentHP/unit.maxHP;
        hpText.text = "HP " + unit.currentHP + "/" + unit.maxHP;
        manaText.text = "Mana " + unit.currentMana + "/" + unit.maxMana;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
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

    public void OnStateUpdate(BattleState state)
    {
        if(state != BattleState.PLAYERTURN)
        {
            classOptions.SetActive(false);
            fallbackText.SetActive(true);
        } else
        {
            fallbackText.SetActive(false);
        }
    }
    #endregion 


}
