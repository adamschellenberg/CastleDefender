using UnityEngine;

public class PlayerPrefsManager 
{
    private const string _highScoreKey = "HighScore";
    private const string _currentScoreKey = "CurrentScore";
    private const string _castleHealthKey = "CastleHealth";

    private static void SetHighScore(int highScore)
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

        int highScore = GetHighScore();

        if (currentScore >= highScore)
        {
            SetHighScore(currentScore);        
        }
    }

    public static int GetCurrentScore()
    {
        return PlayerPrefs.GetInt(_currentScoreKey, 0);
    }

    public static void SetCastleHealth(float health)
    {
        PlayerPrefs.SetFloat(_castleHealthKey, health);
    }

    public static float GetCastleHealth()
    {
        return PlayerPrefs.GetFloat(_castleHealthKey, 0);
    }
}
