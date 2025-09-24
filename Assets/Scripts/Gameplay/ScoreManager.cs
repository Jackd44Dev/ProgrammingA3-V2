using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore;
    public static ScoreManager Instance;

    void Awake()
    {
        Instance = this;    
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
    }

    public int fetchScore()
    {
        return currentScore;
    }
}
