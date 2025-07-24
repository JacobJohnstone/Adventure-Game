using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    [Header("Scene Name")]
    [SerializeField] string levelName;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") { 
            SceneManager.LoadScene(levelName, LoadSceneMode.Single);
        }
    }

}
