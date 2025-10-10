using UnityEngine;

public class TrapContainer : MonoBehaviour
{
    public GameObject[] spawnPositions; // as a scriptable object cannot have per-scene game object data stored inside of it, TrapContainer holds this info
    public TrapData trapData; // the trapData scriptable object holds the rest of the trap info that can exist in any scene (i.e. prefab model, spawn chance, etc)
}
