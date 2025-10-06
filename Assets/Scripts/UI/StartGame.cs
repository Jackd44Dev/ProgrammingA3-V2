using UnityEngine;

public class StartGame : SceneLoader // trying out extending a class
{
    public PlayerData playerData;

    public override void loadNewScene() // this script will wipe any per-run data from PlayerData (but NOT things like total coins or runs completed) before then loading a scene
    {
        playerData.clearRunInfo();
        base.loadNewScene(); // i'm guessing this runs the pre-override method, which is great because the original functionality is still required here
    }
}
