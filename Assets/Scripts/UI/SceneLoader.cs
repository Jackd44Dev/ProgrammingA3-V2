using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string SceneToLoad;
    
    public void loadNewScene()
    {
        Debug.Log("Loading new scene!");
        SceneManager.LoadScene(SceneToLoad);
    }
}
