using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI OutcomeText;
    public TextMeshProUGUI CoinsText;
    public TextMeshProUGUI RunTimeText;
    public TextMeshProUGUI HeightText;
    public string winText = "SUCCESSFUL ESCAPE";
    public string loseText = "YOU DIED!";
    public string winColourHex = "#33FF00";
    public string loseColourHex = "#FF3300";
    public TimeManager timeManager;
    public PlayerData playerData;

    void Start()
    {
        if (playerData.wonRun) // change header based on if player won or lost their run
        {
            OutcomeText.text = "<color=" + winColourHex + ">" + winText + "</color>";
        }
        else
        {
            OutcomeText.text = "<color=" + loseColourHex + ">" + loseText + "</color>";
        }
        int coinsEarned = playerData.coinsEarned;
        CoinsText.text = "Coins Earned: " + coinsEarned.ToString();
        float runTime = timeManager.fetchRunLength();
        RunTimeText.text = "Time Survived: " + runTime.ToString() + "s";
    }
}
