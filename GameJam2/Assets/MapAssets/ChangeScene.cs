using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    [Header("Scene Name")]
    [SerializeField] string levelName;
    public DoorIDs DoorID;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneSpawns.instance.UpdateDoorID(DoorID);
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (Enum.TryParse(sceneName, true, out Levels scene))
        {
            Debug.Log(sceneName + " used: " + scene);
            SceneSpawns.instance.currentLevel = scene;
        }
        else
        {
            Debug.LogWarning("Invalid Level: " + scene);
        }

        if (collision.gameObject.tag == "Player") {
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

}
