using System.Collections;
using UnityEngine;

public class LaserMover : MonoBehaviour
{
    public float distanceToMove; // will always move forward on the Z axis by this much, beam can be rotated to get Z axis facing the right way
    public float timeToReachEnd = 5f;
    Vector3 startPos; // automatically set on start based on where the laser has been placed in-world
    Vector3 endPos; // calculated on start based on distanceToMove, using the Z axis
    bool movingTowardsTarget = true; // keeps track of whether the beam needs to be moving "forwards" or "backwards"
    float lerpProgress = 0f;
    
    void Start()
    {
        startPos = transform.position;
        endPos = startPos + (transform.forward * distanceToMove);
    }

    void Update()
    {
        moveLaser();
    }

    void moveLaser()
    {
        if (movingTowardsTarget) // move forwards
        {
            lerpProgress += Time.deltaTime / timeToReachEnd;
            Vector3 lerpPos = Vector3.Lerp(startPos, endPos, lerpProgress);
            transform.position = lerpPos;
            if (lerpProgress >= 1) // when reaching (or overshooting) the target point, reverse direction
            {
                lerpProgress = 0;
                movingTowardsTarget = false;
                transform.position = endPos; // if laser somehow overshot the target, set it back at its proper end point
            }
            
        }
        else // move backwards
        {
            lerpProgress += Time.deltaTime / timeToReachEnd;
            Vector3 lerpPos = Vector3.Lerp(endPos, startPos, lerpProgress);
            transform.position = lerpPos;
            if (lerpProgress >= 1) // when reaching (or overshooting) the target point, reverse direction
            {
                lerpProgress = 0;
                movingTowardsTarget = true;
                transform.position = startPos; // if laser somehow overshot the target, set it back at its proper end point
            }
        }
    }
}
