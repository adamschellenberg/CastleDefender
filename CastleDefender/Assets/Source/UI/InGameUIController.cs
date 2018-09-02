using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private Slider _castleHealthSlider;

    [SerializeField] private TMP_Text _currentMoneyText;

    [SerializeField] private TMP_Text _fireRateLevelText;
    [SerializeField] private TMP_Text _fireRateCostText;

    [SerializeField] private TMP_Text _damageLevelText;
    [SerializeField] private TMP_Text _damageCostText;

    [SerializeField] private TMP_Text _maxHealthLevelText;
    [SerializeField] private TMP_Text _maxHealthCostText;

    [SerializeField] private TMP_Text _currentHealthCostText;

    [SerializeField] private Button _increaseFireRateButton;
    [SerializeField] private Button _increaseDamageButton;
    [SerializeField] private Button _increaseMaxHealthButton;
    [SerializeField] private Button _increaseHealthButton;

    [Header("Fire Rate")]
    [SerializeField] private float _fireRateIncreaseCost = 1;
    [SerializeField] private float _fireRateIncreaseCostMultiplier = 1.1f;
    [SerializeField] private float _fireRateIncreaseRate = 0.01f;

    [Header("Damage")]
    [SerializeField] private float _damageIncreaseCost = 1;
    [SerializeField] private float _damageIncreaseCostMultiplier = 1.1f;
    [SerializeField] private float _damageIncreaseRate = 0.05f;

    [Header("Max Health")]
    [SerializeField] private float _maxHealthIncreaseCost = 1;
    [SerializeField] private float _maxHealthIncreaseCostMultiplier = 1.1f;
    [SerializeField] private float _maxHealthIncreaseRate = 0.5f;

    [Header("Current Health")]
    [SerializeField] private float _currentHealthIncreaseCost = 1;
    [SerializeField] private float _currentHealthIncreaseCostMultiplier = 1.1f;
    [SerializeField] private float _currentHealthIncreasePercent = 0.1f;

    [Header("Defaults")]
    [SerializeField] private static float _defaultFireRate = 1;
    [SerializeField] private static float _defaultDamage = 1;
    [SerializeField] private static float _defaultMaxHealth = 10;

    private int _currentFireRatelevel;
    private int _currentDamageLevel;
    private int _currentMaxHealthLevel;

    private float _currentFireRateIncreaseCost = 1;
    private float _currentDamageIncreaseCost = 1;
    private float _currentMaxHealthIncreaseCost = 1;
    private float _currentCurrentHealthIncreaseCost = 1;

    private void Start()
    {
        PlayerPrefsManager.SetFireRate(_defaultFireRate);
        PlayerPrefsManager.SetDamage(_defaultDamage);
        PlayerPrefsManager.SetMoney(1000);
        PlayerPrefsManager.SetMaxHealth(_defaultMaxHealth);
        PlayerPrefsManager.SetCurrentHealth(_defaultMaxHealth);
        PlayerPrefsManager.SetCurrentScore(0);

        _increaseFireRateButton.onClick.AddListener(() =>
        {
            _currentFireRatelevel += 1;
            _currentFireRateIncreaseCost *= _fireRateIncreaseCostMultiplier;
            PlayerPrefsManager.IncreaseFireRate(_fireRateIncreaseRate);
            PlayerPrefsManager.DecreaseMoney(_currentFireRateIncreaseCost);
        });

        _increaseDamageButton.onClick.AddListener(() =>
        {
            _currentDamageLevel += 1;
            _currentDamageIncreaseCost *= _damageIncreaseCostMultiplier;
            PlayerPrefsManager.IncreaseDamage(_damageIncreaseRate);
            PlayerPrefsManager.DecreaseMoney(_currentDamageIncreaseCost);
        });

        _increaseMaxHealthButton.onClick.AddListener(() =>
        {
            _currentMaxHealthLevel += 1;
            _currentMaxHealthIncreaseCost *= _maxHealthIncreaseCostMultiplier;
            PlayerPrefsManager.IncreaseMaxHealth(_maxHealthIncreaseRate);
            PlayerPrefsManager.DecreaseMoney(_currentMaxHealthIncreaseCost);
        });

        _increaseHealthButton.onClick.AddListener(() =>
        {
            _currentCurrentHealthIncreaseCost *= _currentHealthIncreaseCostMultiplier;
            PlayerPrefsManager.IncreaseMaxHealth(_currentHealthIncreasePercent);
            PlayerPrefsManager.DecreaseMoney(_currentCurrentHealthIncreaseCost);
        });
    }

    private void Update()
    {
        SetCastleHealthText();
        SetCurrentScoreText();
        SetHighScoreText();
        SetFireRateLevelText();
        SetDamageLevelText();
        SetMaxHealthLevelText();
        SetCurrentMoneyText();

        float currentMoney = PlayerPrefsManager.GetMoney();

        _increaseFireRateButton.interactable = currentMoney > 0 && currentMoney >= _currentFireRateIncreaseCost;
        _increaseDamageButton.interactable = currentMoney > 0 && currentMoney >= _currentDamageIncreaseCost;
        _increaseMaxHealthButton.interactable = currentMoney > 0 && currentMoney >= _currentMaxHealthIncreaseCost;
        _increaseHealthButton.interactable = currentMoney > 0 && currentMoney >= _currentCurrentHealthIncreaseCost && PlayerPrefsManager.GetCurrentHealth() < PlayerPrefsManager.GetMaxHealth();

        _fireRateCostText.text = $"-Fire Rate:\n (-{_defaultFireRate * _fireRateIncreaseRate}) (${_currentFireRateIncreaseCost.ToString("0.00")})";
        _damageCostText.text = $"+Damage:\n (+{_defaultDamage * _damageIncreaseRate}) (${_currentDamageIncreaseCost.ToString("0.00")})";
        _maxHealthCostText.text = $"+Max HP:\n (+({_defaultMaxHealth * _maxHealthIncreaseRate}) ${_currentMaxHealthIncreaseCost.ToString("0.00")})";
        _currentHealthCostText.text = $"+HP:\n (+{_defaultMaxHealth * _currentHealthIncreasePercent}%) (${_currentCurrentHealthIncreaseCost.ToString("0.00")})";
    }

    public void SetCurrentMoneyText()
    {
        float currentMoney = PlayerPrefsManager.GetMoney();
        _currentMoneyText.text = $"${currentMoney.ToString("0.00")}";
    }

    public void SetFireRateLevelText()
    {
        _fireRateLevelText.text = $"Fire Rate: {_currentFireRatelevel}";
    }

    public void SetDamageLevelText()
    {
        _damageLevelText.text = $"Damage: {_currentDamageLevel}";
    }

    public void SetMaxHealthLevelText()
    {
        _maxHealthLevelText.text = $"Max Health: {_currentMaxHealthLevel}";
    }

    public void SetCastleHealthText()
    {
        float castleHealth = PlayerPrefsManager.GetCurrentHealth();
        float castleMaxHealth = PlayerPrefsManager.GetMaxHealth();

        _castleHealthSlider.value = castleHealth / castleMaxHealth;
    }

    public void SetHighScoreText()
    {
        int highScore = PlayerPrefsManager.GetHighScore();
        _highScoreText.text = $"High Score: {highScore.ToString("00000")}";
    }

    public void SetCurrentScoreText()
    {
        int currentScore = PlayerPrefsManager.GetCurrentScore();
        _currentScoreText.text = $"Score: {currentScore.ToString("00000")}";
    }
}
