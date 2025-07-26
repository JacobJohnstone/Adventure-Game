using UnityEngine;
using UnityEngine.UI;

public class WorldHealthBar : MonoBehaviour
{
    Slider hpSlider;
    Text hpText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hpText = gameObject.GetComponentsInChildren<Text>()[0];
        hpSlider = gameObject.GetComponentsInChildren<Slider>()[0];

        hpSlider.value = MainManager.instance.currentHP/MainManager.instance.maxHP;
        hpText.text = "HP " + MainManager.instance.currentHP + "/" + MainManager.instance.maxHP;

    }
}
