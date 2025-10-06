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

        Vector3 lavaSpawnPosition = new Vector3(0, lavaStartHeight, 0); // set lava at its proper start height
        lava = Instantiate(lavaPrefab, lavaSpawnPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        float lavaHeight = lava.transform.position.y + (lavaRiseRate * Time.deltaTime); // calculate the lava's new height
        calculateLavaHeight(lavaHeight);
        lava.transform.position = new Vector3(0, lavaHeight, 0); // slowly rise the lava's in-world position
    }

    void calculateLavaHeight(float newTargetLavaHeight) // increases the lava's height as it physically moves up in the world
    {
        float lavaHeightIncrease = newTargetLavaHeight - lava.transform.position.y;
        playerData.lavaHeight += lavaHeightIncrease;
    }
}
