using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenuController : MonoBehaviour 
{
    [SerializeField] private Button _playAgainButton;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _currentScoreText;

    private void Awake()
    {
        _playAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
        _backToMenuButton.onClick.AddListener(OnBackToMenuButtonClicked);
    }

    public void OnPlayAgainButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnBackToMenuButtonClicked()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void SetHighScoreText()
    {
        int highScore = PlayerPrefsManager.GetHighScore();
        _highScoreText.text = highScore.ToString("00000");
    }

    public void SetCurrentScore()
    {
        int currentScore = PlayerPrefsManager.GetCurrentScore();
        _currentScoreText.text = currentScore.ToString("00000");
    }
}
