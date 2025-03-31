using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Button : MonoBehaviour
{
    public void StartLevel()
    {
        SceneManager.LoadScene("Level_1");
    }
    
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
