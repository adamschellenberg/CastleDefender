using UnityEngine;

public class PlayerPrefsManager 
{
    private const string _highScoreKey = "HighScore";
    private const string _currentScoreKey = "CurrentScore";

    public static void SetHighScore(int highScore)
    {
        PlayerPrefs.SetInt(_highScoreKey, highScore);
    }

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(_highScoreKey, 0);
    }

    public static void SetCurrentScore(int currentScore)
    {
        PlayerPrefs.SetInt(_currentScoreKey, currentScore);
    }

    public static int GetCurrentScore()
    {
        return PlayerPrefs.GetInt(_currentScoreKey, 0);
    }
}
