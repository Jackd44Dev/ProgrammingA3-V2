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
    public TextMeshProUGUI heightUI;
    public TextMeshProUGUI lavaHeightUI;
    public PlayerData playerData;
    public TimeManager timeManager;
    public int heightRequiredToWin = 50;
    public float difficultyHeightIncreaseModifier = 0.1f; // floorScore * this variable (i.e. 0.1 is 10% of floorScore) added as bonus height when completing a floor
    public int floorScore = 0;
    public int numRoomLayouts = 2; // the room layouts to pick between, this number will be appended to "Layout" i.e. room 2 will be the scene named Layout2
    public ParticleSystem hurtVFX;
    public AudioClip hurtSFX;
    public AudioClip powerupSFX;
    public Vector3 respawnPoint;

    AudioSource AudioSource;
    bool spikeDamageOnCooldown = false; // set to true while player is immune to damage from traps
    bool stopUpdatingUI = false; // stop the height UI from jumping up on the last frame before loading a new scene (this annoyed me more than it should)
    
    void Awake()
    {
        instance = this;
        AudioSource = GetComponent<AudioSource>(); 
    }

    void Start()
    {
        if (playerData.floorsCompleted == 0) // if starting a new run (no floors have been completed yet)
        {
            timeManager.startNewRun(); // time manager begins tracking the run with a fresh time
        }
        updateHealthUI();
        updateHeightUIs();
    }

    void Update()
    {
        if (!stopUpdatingUI) { updateHeightUIs(); } // only update height UI if stopUpdatingUI is *NOT* true    
    }

    public void endOfFloor(bool playerDied) // ends the game, bool is true if player won, false if they died
    {
        Cursor.lockState = CursorLockMode.None; // returns control of mouse to player
        Cursor.visible = true;
        stopUpdatingUI = true;
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
            if (playerData.baseHeight >= heightRequiredToWin && !playerData.endlessMode) // player needs to beat multiple levels to raise their height enough to win, also won't work in endless mode
            {
                timeManager.concludeRun(); // stop tracking the time
                playerData.runConcluded(true); // tell playerData that the run is over, and the player has won
                SceneManager.LoadScene("GameOver"); // the game over screen will read playerData to see whether to display won or loss text, which is set on the line above in runConcluded
            }
            else // if the player made it to the exit door, but has not yet met the conditions for winning the run, load a new floor for them
            {
                increaseHeight();
                string randomSceneToLoadNext = "Layout" + UnityEngine.Random.Range(1, numRoomLayouts + 1); // have to have roomLayouts + 1, as max is exclusive in random.range
                SceneManager.LoadScene(randomSceneToLoadNext); // otherwise, load a new level and keep playing!
            }
        }
    }

    void increaseHeight()
    {
        /* float difficultyRandomOffset = UnityEngine.Random.Range((floorScore / 2), floorScore); // using 2 different maths apparently, so UnityEngine.Random is needed instead of just Random.Range
        int difficultyBonusHeight = Mathf.RoundToInt(difficultyRandomOffset); // floor difficulty grants a bonus to height gained, floorScore is set from TrapManager.cs  */
        // the above was scrapped because it was too inconsistent and made the game too hard

        playerData.baseHeight = playerData.height + (floorScore * difficultyHeightIncreaseModifier); // apply the new height to playerData
    }

    public void damagePlayer()
    {
        if (spikeDamageOnCooldown) { return; } // don't double-fire damage events by triggering a cooldown
        StartCoroutine(spikeTrapCooldown());
        playerData.currentHealth -= 1;
        updateHealthUI();
        if (playerData.currentHealth <= 0) // when out of health, game over
        {
            healthUI.enabled = false;
            endOfFloor(true);
        }
        else // only play SFX and VFX if player is still alive
        {
            int healthLost = playerData.maxHealth - playerData.currentHealth;
            Debug.Log("Total health lost: " + healthLost);
            AudioSource.PlayOneShot(hurtSFX);
            for (int i = 1; i <= healthLost; i++) // play the particle effect once for each stack of damage taken, so twice when hit for the second time (more blood particles the lower the player's health)
            {
                hurtVFX.Play();
            }
        }
    }

    public void warpPlayerToStart(PlayerController player)
    {
        damagePlayer();
        player.transform.position = respawnPoint;
    }

    IEnumerator spikeTrapCooldown() // triggers a short cooldown after hitting a spike trap so damage instances can't be stacked (I know this as debouncing, I'm not sure if this is an official term)
    {
        spikeDamageOnCooldown = true;
        yield return new WaitForSeconds(0.5f);
        spikeDamageOnCooldown = false;
    }

    void updateHealthUI()
    {
        healthUI.text = "HEALTH: " + playerData.currentHealth + "/" + playerData.maxHealth;
    }

    void updateHeightUIs()
    {
        if (playerData.baseHeight > heightRequiredToWin && !playerData.endlessMode) // when on the final floor (and not in endless), let the player know they've almost won
        {
            heightUI.text = "HEIGHT: " + playerData.height.ToString("0") + "m - Clear floor to win!";
        }
        else
        {
            heightUI.text = "HEIGHT: " + playerData.height.ToString("0") + "m";
        }
        if (playerData.lavaFrozen)
        {
            lavaHeightUI.text = "LAVA: " + playerData.lavaHeight.ToString("0") + "m (FROZEN)";
        }
        else 
        { 
            lavaHeightUI.text = "LAVA: " + playerData.lavaHeight.ToString("0") + "m";
        }
    }

    public void playPowerupSFX()
    {
        AudioSource.PlayOneShot(powerupSFX);
    }

    public void freezeLava()
    { 
        StartCoroutine(runLavaFreeze());
    }

    public IEnumerator runLavaFreeze()
    {
        playerData.lavaFrozen = true;
        yield return new WaitForSeconds(5f);
        Debug.Log("Unfreezing lava");
        playerData.lavaFrozen = false;
    }
}