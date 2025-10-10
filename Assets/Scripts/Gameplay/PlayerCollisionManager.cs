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
                GM.endOfFloor(false); // endOfFloor takes a "playerDied" bool parameter, so when reaching a win door, pass a false value!
                break;
            case "Lava":
                GM.endOfFloor(true); // making contact with the lava is instant death, so pass playerDied as true here
                break;
            case "Trap":
                GM.damagePlayer(); // tell GameManager to decrease the player's health by 1 and trigger a damage cooldown (GameManager will handle if the player is now out of health)
                break;
            case "SafetyNet":
                GM.warpPlayerToStart(Controller);
                break;
            default:

                break;
        }
    }
}
