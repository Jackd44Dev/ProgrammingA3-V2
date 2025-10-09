using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void quitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
