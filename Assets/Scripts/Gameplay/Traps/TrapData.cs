using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TrapData", menuName = "Scriptable Objects/TrapData")]
public class TrapData : ScriptableObject
{
    [Header("Trap Information")]
    public GameObject trapPrefab;
    public float baseSpawnChance;
    public int difficultyIncreasePerTrap;
}