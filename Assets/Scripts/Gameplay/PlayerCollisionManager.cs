using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    public PlayerController Controller;
    public GameManager GM;

    private void Start()
    {
        GM = GameManager.GameplayManager;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) {
            case "Ground":
                Controller.isGrounded = true;
                break;
            case "WinDoor":
                GM.winGame();
                break;
            case "Lava":
                GM.gameOver();
                break;
            case "Trap":
                GM.damagePlayer();
                break;
            default:

                break;
        }
    }
}
