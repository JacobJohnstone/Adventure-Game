using UnityEngine;

public enum DoorIDs { level0Spawn, returnLevel0, enterLevel1, wonLevel1Combat, returnLevel1, enterLevel2, wonLevel2Combat, returnLevel2, enterLevel3 }
public enum Levels {  level_1, level_2, level_3 }

public class SceneSpawns : MonoBehaviour
{
    public static SceneSpawns instance;

    public DoorIDs doorID = DoorIDs.level0Spawn;
    public Levels currentLevel = Levels.level_1; // used in the combat battle script to know which combat we are in, what scene to load again after the battle

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

    public void UpdateCurrentLevel(Levels level)
    {
        currentLevel = level;
    }
}
