using UnityEngine;

public class TrapManager : MonoBehaviour // sorry, this class is CAKED in comments, i had a real hard time getting this one together, but got there in the end :)
{  
    public GameObject[] trapsToSpawn; // contains empty GameObjects that contain a TrapContainer script, that script holds spawn positions and trap data (prefab, difficulty per trap and spawn chance)
    GameManager manager;
    
    public int roomDifficultyBase; // how difficult this particular room layout is, without any traps

    int roomDifficulty = 0; // used to calculate how hard the current room is

    void Start()
    {
        manager = GameManager.instance;
        roomDifficulty += roomDifficultyBase;
        foreach (var trap in trapsToSpawn) // for this game prototype, this will only ever be the laser and spike trap. but it's very easy to add to this with new trap types!
        {
            int numberOfTrapsSpawned = spawnTraps(trap);
            int difficultyIncreasePerTrap = trap.GetComponent<TrapContainer>().trapData.difficultyIncreasePerTrap;
            int roomDifficultyIncrease = numberOfTrapsSpawned * difficultyIncreasePerTrap;
            roomDifficulty += roomDifficultyIncrease;
        }
        Debug.Log("Room difficulty is: " +  roomDifficulty);
        manager.floorScore += roomDifficulty;
    }

    int spawnTraps(GameObject trapObject) // uses a TrapContainer to spawn all of one type of trap (i.e. spike trap), then returns how many traps of this type were spawned
    {
        int trapsSpawned = 0;
        TrapContainer trapContainer = trapObject.GetComponent<TrapContainer>(); // the trap container contains all the trap's required info
        GameObject[] trapSpawns = trapContainer.spawnPositions; // spawn locations are in a MonoBehaviour script, as ScriptableObjects can't hold scene GameObjects, only prefabs
        TrapData trapData = trapContainer.trapData; // trapData is global and can be reused across scenes easily

        foreach (var trapSpawnPoint in trapSpawns)
        {
            int RandomRoll = Random.Range(1, 100); // these numbers are inclusive, so 0 to 100 would result in 101 different numbers that can be picked
            float spawnChance = trapData.baseSpawnChance; // obtain the trap's base chance to spawn
            Transform spawnPointTransform = trapSpawnPoint.transform; // find where the trap needs to be placed in the world if the spawn chance is rolled
            if (RandomRoll <= spawnChance) // i.e if spawn chance is 10 (as in, 10%) and we roll a 5, spawn the object
            {
                trapsSpawned++;
                Instantiate(trapData.trapPrefab, spawnPointTransform.position, spawnPointTransform.rotation);
            }
        }
        return trapsSpawned;
    }
}
