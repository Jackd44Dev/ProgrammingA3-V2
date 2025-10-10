using UnityEngine;

public class LavaManager : MonoBehaviour
{
    public GameObject lavaPrefab; 
    GameObject lava; // a reference to the newly instantiated lava in this scene
    public float lavaRiseRate = 0.5f;
    public float lavaStartHeight = 0f; // this might look silly, but this value will be placed somewhere between 0 and -10 a little further down in this script
    public PlayerData playerData;
    public float lavaVirtualRiseRateModifier = 1.5f;
    bool lavaSpawned = false; // tracks whether the lava has been spawned in this scene yet

    void Start()
    {
        checkToSpawnLava();
    }

    void Update()
    {
        if (playerData.lavaFrozen) { return; } // don't rise lava if lava freeze powerup is currently active
        if (lavaSpawned)
        {
            raiseLava();
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
        if (playerData.baseHeight <= (10 + playerData.lavaHeight)) // if the lava is 10m or closer to the floor's *base* height (not the player's current height!), spawn the lava
        {
            lavaSpawned = true;
            float lavaHeightOffset = Mathf.Clamp(playerData.baseHeight - playerData.lavaHeight, 0, 10); // clamps the distance between the base height and the lava, higher value means the lava spawns lower! 
            lavaStartHeight -= lavaHeightOffset; // the lava will spawn at y = 0 by default, so this line will instead make the lava spawn further down from the player, based on lavaHeightOffset
            Vector3 lavaSpawnPosition = new Vector3(0, lavaStartHeight, 0); // set lava at its adjusted start height
            lava = Instantiate(lavaPrefab, lavaSpawnPosition, Quaternion.identity);
            lava.GetComponent<MeshRenderer>().material = playerData.lavaMaterials[playerData.selectedCosmetic]; // edit the lava's material to the chosen cosmetic
        }
    }

    void raiseLava()
    {
        float amountToRaiseBy = (lavaRiseRate * Time.deltaTime);
        float lavaHeight = lava.transform.position.y + amountToRaiseBy; // calculate the lava's new height
        playerData.lavaHeight += amountToRaiseBy; // increase the playerData lava tracker to keep it in sync
        lava.transform.position = new Vector3(0, lavaHeight, 0); // slowly rise the lava's in-world position
    }

    void simulateLavaHeight() // increases lavaHeight if the lava is not physically present in the world
    {
        playerData.lavaHeight += (lavaRiseRate * Time.deltaTime * lavaVirtualRiseRateModifier);
    }
}
