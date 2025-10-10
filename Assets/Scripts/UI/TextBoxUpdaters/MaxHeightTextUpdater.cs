using UnityEngine;

public class MaxHeightTextUpdater : TextUpdater
{

    void Start()
    {
        text = "BEST HEIGHT: " + playerData.maxHeightReached + "m";
        updateTextbox();
    }

    public override void updateTextbox()
    {
        base.updateTextbox();
    }
}
