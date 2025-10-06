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

    [Header("Run Info")] // player related per-run info, wiped at the start of a new run
    public bool wonRun;
    public int coinsEarned;
    public int currentHealth;
    public int floorsCompleted;
    public float height;
    public float lavaHeight;

    public void clearRunInfo() // used when starting a new run, all data that carries across scenes (that pertains to an individual run) are wiped
    {
        wonRun = false;
        coinsEarned = 0;
        currentHealth = maxHealth;
        floorsCompleted = 0;
        height = 0f;
        lavaHeight = 0f;
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

}