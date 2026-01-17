using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // Поле остаётся приватным
    [SerializeField] TextMeshProUGUI scoreText;

    // Публичное свойство для доступа к значению score


    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
}
