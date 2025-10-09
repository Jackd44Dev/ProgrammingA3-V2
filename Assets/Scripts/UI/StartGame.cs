using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : SceneLoader // trying out extending a class
{
    public PlayerData playerData;
    public int availableGameScenes = 2;
    public bool enteringEndlessMode = false;

    void Start()
    {
        if (enteringEndlessMode && playerData.coins < 100)
        {
            gameObject.SetActive(false); // only show the endless button when player has 100 coins or more
        }
    }

    public override void loadNewScene() // this script will wipe any per-run data from PlayerData (but NOT things like total coins or runs completed) before then loading a scene
    {
        string gameSceneToLoad = "Layout" + Random.Range(1, availableGameScenes + 1); // +1 because random.range max is exclusive, not inclusive
        playerData.clearRunInfo();
        playerData.endlessMode = enteringEndlessMode;
        SceneManager.LoadScene(gameSceneToLoad);
    }
}
