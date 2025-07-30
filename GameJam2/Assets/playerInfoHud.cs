using UnityEngine;
using UnityEngine.UI;

public class playerInfoHud : MonoBehaviour
{
    int playerLevel;
    public Text levelText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (MainManager.instance.xp > 400)
        {
            playerLevel = 5;
        }
        else if (MainManager.instance.xp > 300)
        {
            playerLevel = 4;
        }
        else if (MainManager.instance.xp > 200)
        {
            playerLevel = 3;
        }
        else if (MainManager.instance.xp > 100)
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
