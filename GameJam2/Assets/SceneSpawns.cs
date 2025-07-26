using UnityEngine;

public enum DoorIDs { level0Spawn, returnLevel0, enterLevel1, wonLevel1Combat, returnLevel1, enterLevel2, wonLevel2Combat, returnLevel2, enterLevel3 }

public class SceneSpawns : MonoBehaviour
{
    public static SceneSpawns instance;

    public DoorIDs doorID = DoorIDs.level0Spawn;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

    }

    public void UpdateDoorID(DoorIDs lastDoorID)
    {
        doorID = lastDoorID;
    }
}
