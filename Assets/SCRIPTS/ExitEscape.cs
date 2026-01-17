using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitEscape : MonoBehaviour
{
    [SerializeField] GameObject escape;
    [SerializeField] GameObject levelsecond;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        escape.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            escape.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1f;
            escape.SetActive(false);
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ContiniueGameThatPlaying()
    {
        Time.timeScale = 1f;
        levelsecond.SetActive(false);
        escape.SetActive(false);
    }

    // Новый метод для закрытия escape через кнопку
    public void CloseEscapeMenu()
    {
        Time.timeScale = 1f; // Снимаем паузу
        escape.SetActive(false); // Скрываем меню паузы
    }
}
