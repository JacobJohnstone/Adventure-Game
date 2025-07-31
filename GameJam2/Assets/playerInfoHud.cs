using UnityEngine;
using UnityEngine.UI;

public class playerInfoHud : MonoBehaviour
{
    int playerLevel;
    public Text levelText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (MainManager.instance.level > 4)
        {
            playerLevel = 5;
        }
        else if (MainManager.instance.level > 3)
        {
            playerLevel = 4;
        }
        else if (MainManager.instance.level > 2)
        {
            playerLevel = 3;
        }
        else if (MainManager.instance.level > 1)
        {
            playerLevel = 2;
        }
        else
        {
            playerLevel = 1;
        }

        levelText.text = "Level " + playerLevel;

    }



}
