using UnityEngine;

public class LavaManager : MonoBehaviour
{
    public GameObject lavaPrefab; 
    GameObject lava; // a reference to the newly instantiated lava in this scene
    public float lavaRiseRate = 0.5f;
    public float lavaStartHeight = -10f;
    public PlayerData playerData;
    bool lavaSpawned = false;

    void Start()
    {
        checkToSpawnLava();
    }

    void Update()
    {
        if (lavaSpawned)
        {
            float lavaHeight = lava.transform.position.y + (lavaRiseRate * Time.deltaTime); // calculate the lava's new height
            calculateLavaHeight(lavaHeight); // add the lava's height to playerData, comparing the old height to the float above (so this line CANNOT be below the next line)
            lava.transform.position = new Vector3(0, lavaHeight, 0); // slowly rise the lava's in-world position
        }
        else // if lava hasn't spawned yet because it's too far away, its height increase is simulated (at a faster 1.5x rate!) until it is close enough to be spawned
        {
            simulateLavaHeight();
            checkToSpawnLava();
        }
    }

    void checkToSpawnLava()
    {
        if (lavaSpawned) { return; }
        if (playerData.baseHeight <= (10 + playerData.lavaHeight)) // if the lava is 10m or closer to the floor's base height, spawn the lava
        {
            lavaSpawned = true;
            float physicalLavaOffset = Mathf.Clamp(10 - (playerData.height - playerData.lavaHeight), 0, 10); // if lava height is less than 10 height away from the player, spawn it higher up, closer to the player 
            lavaStartHeight += physicalLavaOffset; // add the offset to the starting height
            Vector3 lavaSpawnPosition = new Vector3(0, lavaStartHeight, 0); // set lava at its proper start height
            lava = Instantiate(lavaPrefab, lavaSpawnPosition, Quaternion.identity);
        }
    }

    void calculateLavaHeight(float newTargetLavaHeight) // increases playerData's tracked lavaHeight as the lava physically moves up in the world
    {
        float lavaHeightIncrease = newTargetLavaHeight - lava.transform.position.y;
        playerData.lavaHeight += lavaHeightIncrease;
    }

    void simulateLavaHeight() // increases lavaHeight if the lava is not physically present in the world
    {
        playerData.lavaHeight += (lavaRiseRate * Time.deltaTime * 1.5f);
    }
}
