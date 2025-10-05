using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    public PlayerController Controller;
    public GameManager GM;

    private void Start()
    {
        GM = GameManager.instance;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) {
            case "Ground":
                Controller.isGrounded = true; // player is back on the ground, so they may jump again!
                break;
            case "WinDoor":
                GM.endGame(true); // end the game with a win result when reaching a victory door
                break;
            case "Lava":
                GM.endGame(false); // end the game with a loss result upon contact with lava
                break;
            case "Trap":
                GM.damagePlayer(); // tell gamemanager to decrease the player's health by 1 and trigger a damage cooldown
                break;
            default:

                break;
        }
    }
}
