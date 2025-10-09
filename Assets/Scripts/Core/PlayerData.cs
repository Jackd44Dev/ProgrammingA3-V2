using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Player Stats")] // the player's persistent stats
    public int runsComplete; // stat tracking on how many runs have been won
    public int maxHeightReached; // basically a high score for height reached
    public int coins; // this was originally just score, but I wanted some kind of meta progression so it's now coins!
    public int maxHealth = 3; // a shop was originally planned where you could buy max health upgrades, but had to be scrapped due to time constraints, feel free to edit this value if you'd like :)

    [Header("Cosmetics")] // the player's owned cosmetics
    public Material[] lavaMaterials;
    public int lastCosmeticUnlocked = 0;
    public int[] coinsRequiredForCosmetics;
    public int selectedCosmetic = 0;

    [Header("Run Info")] // player related per-run info, wiped at the start of a new run - realistically this should be a new scriptable object, but alas, time constraints.
    public bool wonRun;
    public int coinsEarned;
    public int currentHealth;
    public int floorsCompleted;
    public float baseHeight; // a baseline new height, set each time the player climbs a floor
    public int height; // this is the player's "true" height, adding their current Y position to baseHeight
    public float lavaHeight;
    public bool endlessMode;

    public void clearRunInfo() // used when starting a new run, all data that carries across scenes (that pertains to an individual run) are wiped
    {
        wonRun = false;
        coinsEarned = 0;
        currentHealth = maxHealth;
        floorsCompleted = 0;
        baseHeight = 0f;
        height = 0;
        lavaHeight = -10f;
        endlessMode = false;
    }

    public void runConcluded(bool playerWon) // handles some high score tracking and cosmetic unlock functionality
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

        if (height > maxHeightReached)
        {
            maxHeightReached = height; // set a new height record if one was reached in the last run
        }
        coins += coinsEarned;
        checkForUnlockedCosmetics();
    }

    void checkForUnlockedCosmetics()
    {
        int cosmeticRequirementIndex = 0; // keeps track of what cycle of the foreach loop we are currently in
        foreach (int i in coinsRequiredForCosmetics)
        {
            if (coins > i && lastCosmeticUnlocked < cosmeticRequirementIndex) // if enough coins to unlock skin, and skin is not currently unlocked
            {
                lastCosmeticUnlocked = cosmeticRequirementIndex; // set unlocked cosmetic at the new index (i.e. this int at 2 would mean skins 0, 1 and 2 in the array are unlocked)
            }
            cosmeticRequirementIndex++;
        }
    }

    public void offsetHeight(float playerYPos) // sets the player's true height, based on Y offset from baseHeight. is rounded to whole numbers
    {
        height = Mathf.RoundToInt(baseHeight + playerYPos);
    }
}