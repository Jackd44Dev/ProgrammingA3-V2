using UnityEngine;

public class HealthPowerup : PickupBase
{
    public PlayerData playerData;

    public override void onPowerupPickup()
    {
        if (playerData.currentHealth < playerData.maxHealth) // don't give health if already at max
        {
            playerData.currentHealth++;
        }
    }
}
