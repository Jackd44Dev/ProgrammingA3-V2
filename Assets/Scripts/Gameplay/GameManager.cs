using System;
using System.Collections;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI healthUI;
    public PlayerData playerData;
    public TimeManager timeManager;
    public int heightRequiredToWin = 50;
    public int floorScore = 0;

    bool spikeDamageOnCooldown = false; // set to true while player is immune to damage from traps
    
    void Awake()
    {
        instance = this;
        
    }

    void Start()
    {
        if (playerData.floorsCompleted == 0) // if starting a new run (no floors have been completed yet)
        {
            timeManager.startNewRun(); // time manager begins tracking the run with a fresh time
        }
    }

    public void endOfFloor(bool playerDied) // ends the game, bool is true if player won, false if they died
    {
        Cursor.lockState = CursorLockMode.None; // returns control of mouse to player
        Cursor.visible = true;
        if (playerDied) // if the current floor ended via player death, end the run here
        {
            timeManager.concludeRun(); // stop tracking the time
            playerData.runConcluded(false); // tells playerData that the run is over and the player did NOT win
            SceneManager.LoadScene("GameOver");
        }
        else // if player got to the exit door, determine if they have won or if they need to keep climbing floors
        {
            playerData.coinsEarned += floorScore; // only 'bank' score from this floor if they player actually completed it, if they die they earn nothing
            playerData.floorsCompleted++;
            if (playerData.baseHeight >= heightRequiredToWin) // player needs to beat multiple levels to raise their height enough to win
            {
                timeManager.concludeRun(); // stop tracking the time
                playerData.runConcluded(true); // tell playerData that the run is over, and the player has won
                SceneManager.LoadScene("GameOver"); // the game over screen will read playerData to see whether to display won or loss text, which is set on the line above in runConcluded
            }
            else // if the player made it to the exit door, but has not yet met the conditions for winning the run, load a new floor for them
            {
                increaseHeight();
                SceneManager.LoadScene("Test"); // otherwise, load a new level and keep playing!
            }
        }
    }

    void increaseHeight()
    {
        /* float difficultyRandomOffset = UnityEngine.Random.Range((floorScore / 2), floorScore); // using 2 different maths apparently, so UnityEngine.Random is needed instead of just Random.Range
        int difficultyBonusHeight = Mathf.RoundToInt(difficultyRandomOffset); // floor difficulty grants a bonus to height gained, floorScore is set from TrapManager.cs  */
        // the above was scrapped because it was too inconsistent and made the game too hard

        playerData.baseHeight = playerData.height + floorScore; // apply the new height to playerData
    }

    public void damagePlayer()
    {
        if (spikeDamageOnCooldown) { return; } // don't double-fire damage events by triggering a cooldown
        StartCoroutine(spikeTrapCooldown());
        playerData.currentHealth -= 1;
        healthUI.text = "HEALTH: " + playerData.currentHealth + "/" + playerData.maxHealth;
        if (playerData.currentHealth <= 0) // when out of health, game over
        {
            healthUI.enabled = false;
            endOfFloor(false);
        }
    }

    IEnumerator spikeTrapCooldown() // triggers a short cooldown after hitting a spike trap so damage instances can't be stacked (I know this as debouncing, I'm not sure if this is an official term)
    {
        spikeDamageOnCooldown = true;
        yield return new WaitForSeconds(0.5f);
        spikeDamageOnCooldown = false;
    }

}