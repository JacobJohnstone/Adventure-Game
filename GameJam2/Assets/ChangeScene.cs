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

        if (collision.gameObject.tag == "Player") { 
            SceneManager.LoadScene(levelName, LoadSceneMode.Single);
        }
    }

}
