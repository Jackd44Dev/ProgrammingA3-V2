using UnityEngine;

public class RunsTextUpdater : TextUpdater
{
    void Start()
    {
        text = "# RUNS WON: " + playerData.runsComplete.ToString();
        updateTextbox();
    }

    public override void updateTextbox()
    {
        base.updateTextbox();
    }
}
