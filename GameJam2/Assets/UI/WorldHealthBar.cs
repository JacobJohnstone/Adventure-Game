using UnityEngine;
using UnityEngine.UI;

public class WorldHealthBar : MonoBehaviour
{
    [SerializeField] Slider hpSlider;
    Text hpText;

    int lastHPCheck = MainManager.instance.currentHP;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hpText = gameObject.GetComponentsInChildren<Text>()[0];

        hpSlider.value = (float)MainManager.instance.currentHP/MainManager.instance.maxHP;
        hpText.text = "HP " + MainManager.instance.currentHP + "/" + MainManager.instance.maxHP;

    }

    private void Update()
    {
        if (lastHPCheck != MainManager.instance.currentHP)
        {
            hpSlider.value = (float)MainManager.instance.currentHP / MainManager.instance.maxHP;
            hpText.text = "HP " + MainManager.instance.currentHP + "/" + MainManager.instance.maxHP;
        }
    }
}
