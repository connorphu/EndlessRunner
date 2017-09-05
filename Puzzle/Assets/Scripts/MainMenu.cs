using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string playGame;
    public string instructions;

    //Main menu.
    public void PlayGame()
    {
        SceneManager.LoadScene(playGame);
    }

    public void Instructions()
    {
        SceneManager.LoadScene(instructions);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
