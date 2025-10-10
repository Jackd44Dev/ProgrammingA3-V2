using UnityEngine;

public class UnpauseGameButton : MonoBehaviour
{
    public PlayerController controller;

    public void unpauseGame()
    {
        if (controller.gameIsPaused)
        {
            controller.pauseGame(); // this method will automatically unpause and hide this UI, if gameIsPaused is already true
        }
        else
        {
            controller.pauseMenu.SetActive(false); // if game is running but this menu is open somehow, pressing this button will just hide the menu instead
        }
    }
}
