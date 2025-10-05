using System.Collections;
using UnityEngine;

public class LaserMover : MonoBehaviour
{
    public float distanceToMove; // will always move forward on the Z axis by this much, beam can be rotated to get Z axis facing the right way
    public float timeToReachEnd = 5f;
    Vector3 startPos; // automatically set on start based on where the laser has been placed in-world
    Vector3 endPos; // calculated on start based on distanceToMove, using the Z axis
    bool movingTowardsTarget = true; // keeps track of whether the beam needs to be moving "forwards" or "backwards"
    
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
        if (movingTowardsTarget) // move forward on the Z axis
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + ((distanceToMove / timeToReachEnd) * Time.deltaTime));
            if (transform.position.z >= endPos.z) // when reaching (or overshooting) the target point, reverse direction
            {
                movingTowardsTarget = false;
                transform.position = endPos; // and if laser overshot the target, set it back at its proper end point
            }
        }
        else // move backwards on the Z axis
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - ((distanceToMove / timeToReachEnd) * Time.deltaTime));
            if (transform.position.z <= startPos.z) // same as above, just in reverse (using start instead of end position)
            {
                movingTowardsTarget = true;
                transform.position = startPos;
            }
        }
    }
}
