using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = player.transform.position;
    }

}
