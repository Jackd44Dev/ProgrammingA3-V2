using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : SceneLoader // trying out extending a class
{
    public PlayerData playerData;
    public int availableGameScenes = 2;

    public override void loadNewScene() // this script will wipe any per-run data from PlayerData (but NOT things like total coins or runs completed) before then loading a scene
    {
        string gameSceneToLoad = "Layout" + Random.Range(1, availableGameScenes + 1); // +1 because random.range max is exclusive, not inclusive
        playerData.clearRunInfo();
        SceneManager.LoadScene(gameSceneToLoad);
    }
}
