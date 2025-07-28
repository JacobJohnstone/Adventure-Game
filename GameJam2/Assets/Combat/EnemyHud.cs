using UnityEngine;
using UnityEngine.UI;


public class EnemyHud : MonoBehaviour
{
    [SerializeField]
    Text nameText;
    [SerializeField]
    Slider hpSlider;
    [SerializeField]
    Text hpText;


    void Start()
    {
        hpSlider.maxValue = 1;
    }

    public void SetHUD(EnemyUnit unit)
    {
        nameText.text = unit.unitName;
        hpSlider.value = (float)unit.currentHP / unit.maxHP;
        hpText.text = "HP " + unit.currentHP + "/" + unit.maxHP;
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

}
