using UnityEngine;

public class Spawner2 : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform enterSpawn;
    public Transform winCombatSpawn;
    public Transform returnSpawn;

    void Start()
    {
        DoorIDs targetID = SceneSpawns.instance.doorID;

        if (targetID == DoorIDs.enterLevel1 || targetID == DoorIDs.enterLevel2 || targetID == DoorIDs.enterLevel3)
        {
            Instantiate(playerPrefab, enterSpawn);
        }
        else if (targetID == DoorIDs.returnLevel1 || targetID == DoorIDs.returnLevel2)
        {
            Instantiate(playerPrefab, returnSpawn);
        } else
        {
            Instantiate(playerPrefab, winCombatSpawn);
        }

        StartCoroutine(SceneTransition.instance.TransitionIn());
    }
}
