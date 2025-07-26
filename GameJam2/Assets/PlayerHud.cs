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

    Text nameText;
    Text levelText;

    [SerializeField]
    Slider hpSlider;

    void Start()
    {
        // Read values from scriptable object
        // I feel like they just work similar to .json files essentially tbh
        // Read name, level, maxHP, currentHP, learnedSkillsArray(?)

        hpSlider.maxValue = 1; // set to maxHP from scriptable object
        hpSlider.value = 0.2f; // currentHP from scriptable object
    }

    public void SetHUD(Unit unit)
    {
        //nameText.text = unit.unitName;
        //levelText.text = "Level " + unit.unitLevel;
        //hpSlider.maxValue = unit.maxHP;
        //hpSlider.value = unit.currentHP;
    }

    public void setHP(int hp)
    {
        hpSlider.value = hp;
    }

    // For UI Class selection/display
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
