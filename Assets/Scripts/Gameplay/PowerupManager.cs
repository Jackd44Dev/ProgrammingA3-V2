using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public GameObject powerupSpawnPoint;
    public PowerupData[] powerupTypes;
    public float powerupSpawnChance = 50f;

    void Start()
    {
        if (Random.Range(1f, 100f) <= powerupSpawnChance) // only spawn a powerup if succeeding the RNG roll
        {
            PowerupData randomPowerup = powerupTypes[Random.Range(0, powerupTypes.Length)];
            Instantiate(randomPowerup.powerupPrefab, powerupSpawnPoint.transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
