using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform initialSpawn;
    public Transform doorSpawn;

    void Start()
    {
        DoorIDs targetID = SceneSpawns.instance.doorID;

        if(targetID == DoorIDs.returnLevel0)
        {
            //Instantiate(playerPrefab, doorSpawn);
        } else
        {
            //Instantiate(playerPrefab, initialSpawn);
        }
    }


}
