using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{

    [Header("Player Info")]
    [SerializeField] Text nameText;

    Text levelText;
    Slider hpSlider;

    void Start()
    {
       // Read values from scriptable object
       // I feel like they just work similar to .json files essentially tbh
    }

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Level " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    public void setHP(int hp)
    {
        hpSlider.value = hp;
    }


}
