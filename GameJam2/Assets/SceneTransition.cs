using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    public bool isComplete = false;
    public GameObject fadeScreen;
    Image image;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        image = fadeScreen.GetComponent<Image>();
        fadeScreen.SetActive(false);
    }

    // Call before scene change
    public IEnumerator TransitionOut()
    {
        fadeScreen.SetActive(true);
        Color tempColor = image.color;
        while (tempColor.a < 1f)
        {
            tempColor.a += 0.01f;
            image.color = tempColor;
            yield return new WaitForSeconds(0.005f);
        }

        tempColor.a = 1f;
        image.color = tempColor;
        isComplete = true;
    }

    // Fades out the black screen -- call in Start() Methods
    public IEnumerator TransitionIn()
    {
        Color tempColor = image.color;
        while (tempColor.a > 0f)
        {
            tempColor.a -= 0.01f;
            image.color = tempColor;
            yield return new WaitForSeconds(0.005f);
        }

        tempColor.a = 0f;
        image.color = tempColor;
        fadeScreen.SetActive(false);
        isComplete = true;
    }
}
