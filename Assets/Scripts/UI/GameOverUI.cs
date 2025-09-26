using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI OutcomeText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI RunTimeText;
    public TextMeshProUGUI HeightText;
    public string winText = "SUCCESSFUL ESCAPE";
    public string loseText = "YOU DIED!";
    public string winColourHex = "#33FF00";
    public string loseColourHex = "#FF3300";

    void Start()
    {
        if (PlayerPrefs.GetInt("PlayerWon") == 1)
        {
            OutcomeText.text = "<color=" + winColourHex + ">" + winText + "</color>";
        }
        else
        {
            OutcomeText.text = "<color=" + loseColourHex + ">" + loseText + "</color>";
        }
        int score = PlayerPrefs.GetInt("Score");
        ScoreText.text = "SCORE: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
