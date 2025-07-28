using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class Menu : MonoBehaviour
{

    public void OnClickStart()
    {
        SceneTransition.instance.isComplete = false;
        StartCoroutine(SceneTransition.instance.TransitionOut());
        StartCoroutine(TransitionScene());
        //SceneManager.LoadScene("Level_0");
    }

    IEnumerator TransitionScene()
    {
        yield return new WaitUntil(() => SceneTransition.instance.isComplete);
        SceneManager.LoadScene("Level_0", LoadSceneMode.Single);
    }

}
