using System.Collections;
using UnityEngine;

public class LaserMover : MonoBehaviour
{
    public float distanceToMove; // will always move forward on the Z axis by this much, beam can be rotated to get Z axis facing the right way
    public float timeToReachEnd = 5f;
    Vector3 startPos;
    Vector3 endPos;
    bool movingTowardsTarget = true;
    
    void Start()
    {
        startPos = transform.position;
        endPos = startPos;
        endPos.z += distanceToMove;
    }

    void Update()
    {
        moveLaser();
    }

    void moveLaser()
    {
        if (movingTowardsTarget)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + ((distanceToMove / timeToReachEnd) * Time.deltaTime));
            if (transform.position.z >= endPos.z)
            {
                movingTowardsTarget = false;
                transform.position = endPos;
            }
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - ((distanceToMove / timeToReachEnd) * Time.deltaTime));
            if (transform.position.z <= startPos.z)
            {
                movingTowardsTarget = true;
                transform.position = startPos;
            }
        }
    }
}
