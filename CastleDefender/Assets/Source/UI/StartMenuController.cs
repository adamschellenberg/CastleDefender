using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private TMP_Text _highScoreText;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonClicked);

        SetHighScoreText();
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }

    public void SetHighScoreText()
    {
        int highScore = PlayerPrefsManager.GetHighScore();
        _highScoreText.text = $"High Score: {highScore.ToString("00000")}";
    }
}
