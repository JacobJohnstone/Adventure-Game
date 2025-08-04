using UnityEngine;
using UnityEngine.UI;

public class updateText : MonoBehaviour
{
    Text levelText;

    int lastLevelCheck = MainManager.instance.level;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelText = gameObject.GetComponentsInChildren<Text>()[0];

        levelText.text = "Level " + MainManager.instance.level;

    }

    private void Update()
    {
        if (lastLevelCheck != MainManager.instance.level)
        {
            levelText.text = "Level " + MainManager.instance.level;
        }
    }
}
