using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void SelectGameStage()
    {
        SceneManager.LoadScene("Stage Select");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
