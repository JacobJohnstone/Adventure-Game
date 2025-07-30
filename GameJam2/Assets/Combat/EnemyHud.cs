using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHud : MonoBehaviour
{

    [SerializeField]
    Slider hpSlider;
    [SerializeField]
    TextMeshProUGUI hpText;


    void Start()
    {
        hpSlider.maxValue = 1;
    }

    public void SetHUD(EnemyUnit unit)
    {
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
