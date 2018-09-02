using UnityEngine;

public class PlayerPrefsManager
{
    private const string _highScoreKey = "HighScore";
    private const string _currentScoreKey = "CurrentScore";
    private const string _moneyKey = "Money";
    private const string _fireRateKey = "FireRate";
    private const string _damageKey = "Damage";
    private const string _maxHealthKey = "MaxHealth";
    private const string _currentHealthKey = "CurrentHealth";

    private static void SetHighScore(int highScore)
    {
        PlayerPrefs.SetInt(_highScoreKey, highScore);
    }

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(_highScoreKey, 0);
    }

    public static void IncreaseCurrentScore(int amount)
    {
        int currentScore = GetCurrentScore();
        currentScore += amount;

        SetCurrentScore(currentScore);
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

    public static void SetMoney(float money)
    {
        PlayerPrefs.SetFloat(_moneyKey, money);
    }

    public static float GetMoney()
    {
        return PlayerPrefs.GetFloat(_moneyKey, 0);
    }

    public static void IncreaseMoney(float amount)
    {
        float currentMoney = GetMoney();
        currentMoney += amount;

        SetMoney(currentMoney);
    }

    public static void DecreaseMoney(float amount)
    {
        float currentMoney = GetMoney();
        currentMoney -= amount;

        SetMoney(currentMoney);
    }

    public static void SetFireRate(float rate)
    {
        PlayerPrefs.SetFloat(_fireRateKey, rate);
    }

    public static float GetFireRate()
    {
        return PlayerPrefs.GetFloat(_fireRateKey, 0);
    }

    public static void IncreaseFireRate(float amount)
    {
        float currentFireRate = GetFireRate();
        currentFireRate -= amount;

        SetFireRate(currentFireRate);
    }

    public static void SetDamage(float amount)
    {
        PlayerPrefs.SetFloat(_damageKey, amount);
    }

    public static float GetDamage()
    {
        return PlayerPrefs.GetFloat(_damageKey, 0);
    }

    public static void IncreaseDamage(float amount)
    {
        float currentDamage = GetDamage();
        currentDamage += amount;

        SetDamage(currentDamage);
    }

    public static void SetMaxHealth(float amount)
    {
        PlayerPrefs.SetFloat(_maxHealthKey, amount);
    }

    public static float GetMaxHealth()
    {
        return PlayerPrefs.GetFloat(_maxHealthKey, 0);
    }

    public static void IncreaseMaxHealth(float amount)
    {
        float currentMaxHealth = GetMaxHealth();
        currentMaxHealth += amount;

		float currentHealth = GetCurrentHealth ();
		SetCurrentHealth (currentHealth + amount);

        SetMaxHealth(currentMaxHealth);
    }

    public static void SetCurrentHealth(float amount)
    {
        PlayerPrefs.SetFloat(_currentHealthKey, amount);
    }

    public static float GetCurrentHealth()
    {
        return PlayerPrefs.GetFloat(_currentHealthKey, 0);
    }

    public static void IncreaseCurrentHealth(float percentOfMaxHealth)
    {
        float currentHealth = GetCurrentHealth();
		float maxHealth = GetMaxHealth ();

		currentHealth += percentOfMaxHealth * maxHealth;

		if (currentHealth > maxHealth) 
		{
			currentHealth = maxHealth;
		}

        SetCurrentHealth(currentHealth);
    }

    public static void DecreaseCurrentHealth(float amount)
    {
        float currentHealth = GetCurrentHealth();
        currentHealth -= amount;

        SetCurrentHealth(currentHealth);
    }
}
