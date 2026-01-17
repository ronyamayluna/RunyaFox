using UnityEngine;
using TMPro; // Подключаем TextMeshPro
using System.Collections;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText; // Используем TMP_Text для TextMeshPro
    [SerializeField] private TextMeshProUGUI gameOverTimer; // Ссылка на Canvas, который будет отображаться при окончании времени
    [SerializeField] private UIManager uiManager;
    [SerializeField] private PlayerController playerController; // Ссылка на PlayerController

    private float elapsedTime = 0f; // Время, прошедшее с начала игры
    private bool isTimerRunning = false; // Флаг для проверки, запущен ли таймер

    private void Start()
    {
        // Скрываем текст таймера в начале игры
        timerText.gameObject.SetActive(false);

        // Проверяем, назначен ли PlayerController
        if (playerController == null)
        {
            Debug.LogError("PlayerController не назначен в Timer! Назначьте его в инспекторе.");
        }
    }

    private void Update()
    {
        // Проверяем, завершена ли игра
        if (playerController != null && playerController.isGameOver)
        {
            if (!playerController.isGameOver) // Чтобы StopTimer() вызывался только один раз
            {
                Debug.Log("Игра завершена. Останавливаем таймер.");
                StopTimer();
                playerController.isGameOver = true; // Устанавливаем флаг, чтобы не вызывать StopTimer() повторно
            }
            return;
        }

        // Если таймер запущен, обновляем время
        if (isTimerRunning && !playerController.isGameOver)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        // Форматируем время в формате MM:SS
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void StartTimer()
    {
        // Показываем текст таймера и запускаем таймер
        timerText.gameObject.SetActive(true);
        isTimerRunning = true;
        elapsedTime = 0f; // Сбрасываем время
    }

    public void StopTimer()
    {
        // Останавливаем таймер
        Debug.Log("Таймер остановлен.");
        isTimerRunning = false;
    }

    public void EndGame()
    {
        playerController.isGameOver = true;
        gameOverTimer.text = timerText.text;
        isTimerRunning = false; // Останавливаем таймер
    }
}
