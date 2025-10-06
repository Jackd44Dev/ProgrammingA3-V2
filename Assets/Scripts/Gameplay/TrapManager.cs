using UnityEngine;

public class TrapManager : MonoBehaviour
{  
    public GameObject[] trapsToSpawn; // contains empty GameObjects that contain a TrapContainer script, that script holds spawn positions and trap data (prefab, difficulty per trap and spawn chance)
    
    public int roomDifficultyBase; // how difficult this particular room layout is, without any traps

    int roomDifficulty = 0; // used to calculate how hard the current room is

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roomDifficulty += roomDifficultyBase;
        foreach (var trap in trapsToSpawn) // for this game prototype, this will only ever be the laser and spike trap. but it's very easy to add to this with new trap types!
        {
            spawnTraps(trap);
        }
    }

    int spawnTraps(GameObject trapObject)
    {
        int trapsSpawned = 0;
        TrapContainer trapContainer = trapObject.GetComponent<TrapContainer>(); // the trap container contains all the trap's info
        GameObject[] trapSpawns = trapContainer.spawnPositions; // spawn locations are in a MonoBehaviour, as ScriptableObjects can't hold scene GameObjects, only prefabs
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

        Debug.Log("Spawned " + trapsSpawned + " traps!");
        return trapsSpawned;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
