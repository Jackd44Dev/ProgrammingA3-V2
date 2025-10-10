using System.Collections;
using UnityEngine;

public class PickupBase : MonoBehaviour
{
    public float spinSpeed = 90f;

    void Update() // makes powerups spin, because a spinning powerup is an attractive powerup
    {
        spin(); 
    }

    public void spin()
    {
        float spinOffset = spinSpeed * Time.deltaTime;
        this.gameObject.transform.rotation *= Quaternion.Euler(0, spinOffset, 0);
    }

    public virtual void onPowerupPickup()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            onPowerupPickup();
            GameManager.instance.playPowerupSFX();
            Destroy(gameObject);
        }
    }
}
