using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class thanksScene : MonoBehaviour
{
    [Header("Scene Name")]
    [SerializeField] string levelName;


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            //SceneManager.LoadScene(levelName, LoadSceneMode.Single);
            SceneTransition.instance.isComplete = false;
            StartCoroutine(SceneTransition.instance.TransitionOut());
            StartCoroutine(TransitionScene(levelName));
        }
    }

    IEnumerator TransitionScene(string levelName)
    {
        yield return new WaitUntil(() => SceneTransition.instance.isComplete);
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

    public void OnClick()
    {
        SceneTransition.instance.isComplete = false;
        StartCoroutine(SceneTransition.instance.TransitionOut());
        StartCoroutine(TransitionScene(levelName));
    }
}
