using UnityEngine;

public class CoinTextUpdater : TextUpdater
{

    void Start()
    {
        text = "COINS: " + playerData.coins.ToString();
        updateTextbox();
    }

    public override void updateTextbox()
    {
        base.updateTextbox();
    }
}
