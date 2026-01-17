using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject startGameCanvas; 
    [SerializeField] private GameObject gameOverCanvas; 
    [SerializeField] private PlayerController playerController; 
    [SerializeField] private Timer timer; // Ссылка на Timer

    private bool isGamePaused = true;

    void Start()
    {
        Time.timeScale = 0f;
        startGameCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
    }

    void Update()
    {
        if (playerController.isGameOver)
        {
            ShowGameOverCanvas();
        }
    }

    public void OnStartButtonPressed()
    {
        isGamePaused = false;
        Time.timeScale = 1f; 
        startGameCanvas.SetActive(false); 

        // Запускаем таймер
        if (timer != null)
        {
            timer.StartTimer();
        }
        else
        {
            Debug.LogError("Timer не назначен в GameUIManager!");
        }
    }

    private void ShowGameOverCanvas()
    {
        Time.timeScale = 1f;
        timer.EndGame();
        gameOverCanvas.SetActive(true); 
    }
}
