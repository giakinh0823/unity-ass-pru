using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void SettingGame()
    {
        //SceneManager.LoadScene("Game");
    }
    public void HighestScore()
    {
        //SceneManager.LoadScene("Game");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
