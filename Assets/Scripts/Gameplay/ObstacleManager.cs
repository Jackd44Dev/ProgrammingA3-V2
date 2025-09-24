using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject lava;
    public float lavaRiseRate = 0.5f;
    public float lavaStartHeight = -10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lava.transform.position = new Vector3(0, lavaStartHeight, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float lavaHeight = lava.transform.position.y + (lavaRiseRate * Time.deltaTime);
        lava.transform.position = new Vector3(0, lavaHeight, 0);
    }
}
