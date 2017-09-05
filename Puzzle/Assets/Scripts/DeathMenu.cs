using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    //Menu upon death.
    public string restartGame;
    public string mainMenu;

    public void RestartGame()
    {
        SceneManager.LoadScene(restartGame);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
