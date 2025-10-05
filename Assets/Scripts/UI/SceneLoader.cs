using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string SceneToLoad;
    
    public void loadNewScene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
