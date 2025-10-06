using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Player Stats")] // the player's persistent stats
    public int runsComplete;
    public int coins;
    public int maxHealth = 3;

    [Header("Upgrades Purchased")] // the player's owned upgrades
    public bool healthUpgrade1 = false;

    [Header("Run Info")] // player related per-run info, wiped at the start of a new run - realistically this should be a new scriptable object, but alas, time constraints.
    public bool wonRun;
    public int coinsEarned;
    public int currentHealth;
    public int floorsCompleted;
    public float baseHeight; // a baseline new height, set each time the player climbs a floor
    public int height; // this is the player's "true" height, adding their current Y position to baseHeight
    public float lavaHeight;

    public void clearRunInfo() // used when starting a new run, all data that carries across scenes (that pertains to an individual run) are wiped
    {
        wonRun = false;
        coinsEarned = 0;
        currentHealth = maxHealth;
        floorsCompleted = 0;
        baseHeight = 0f;
        height = 0;
        lavaHeight = -10f;
    }

    public void runConcluded(bool playerWon)
    {
        if (playerWon)
        {
            wonRun = true;
            runsComplete++;
        }
        else
        {
            wonRun = false;
        }
        coins += coinsEarned;
    }

    public void offsetHeight(float playerYPos) // sets the player's true height, based on Y offset from baseHeight. is rounded to whole numbers
    {
        height = Mathf.RoundToInt(baseHeight + playerYPos);
    }
}