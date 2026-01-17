using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] GameObject gameoverscreen;
    
    [SerializeField] TextMeshProUGUI finalCoinsText; // “екст дл€ отображени€ количества монет
    [SerializeField] GameObject startGameUI;
    [SerializeField] Timer timer;

    void Start()
    {
        Time.timeScale = 1f;
        gameoverscreen.SetActive(false);
    }

    public void ShowGameOverScreen()
    {
        timer.EndGame();
        Time.timeScale = 0f;
        gameoverscreen.SetActive(true);
    }

       
    public void UpdateFinalCoins(int coins)
    {
        if (finalCoinsText != null)
        {
            finalCoinsText.text = "Coins: " + coins;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scene1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContiniueGameThatPlaying()
    {
        Time.timeScale = 1f;
        gameoverscreen.SetActive(false);
    }
}
