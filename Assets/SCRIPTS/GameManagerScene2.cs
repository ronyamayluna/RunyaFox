using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScene2 : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scene1"); // 
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
