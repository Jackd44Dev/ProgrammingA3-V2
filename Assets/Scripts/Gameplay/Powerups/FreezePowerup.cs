using UnityEngine;

public class FreezePowerup : PickupBase
{
    public PlayerData playerData;

    public override void onPowerupPickup()
    {
        if (!playerData.lavaFrozen)
        {
            GameManager.instance.freezeLava();
        }
    }
}