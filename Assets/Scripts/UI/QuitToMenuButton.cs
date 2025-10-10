using UnityEngine;

public class QuitToMenuButton : MonoBehaviour
{
    public void QuitToMenu()
    {
        Time.timeScale = 1f; // unpause game before heading to menu
        GameManager.instance.endOfFloor(true);
    }
}
