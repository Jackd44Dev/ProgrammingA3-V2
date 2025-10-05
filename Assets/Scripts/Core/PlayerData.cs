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