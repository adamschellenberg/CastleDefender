using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private Slider _castleHealthSlider;

    private void Update()
    {
        SetCastleHealth();
        SetCurrentScoreText();
        SetHighScoreText();
    }

    public void SetCastleHealth()
    {
        float castleHealth = PlayerPrefsManager.GetCastleHealth();
        _castleHealthSlider.value = castleHealth / 100;
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
