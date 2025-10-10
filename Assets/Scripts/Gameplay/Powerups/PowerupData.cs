using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerupData", menuName = "Scriptable Objects/PowerupData")]
public class PowerupData : ScriptableObject
{
    [Header("Powerup Information")]
    public GameObject powerupPrefab;
}