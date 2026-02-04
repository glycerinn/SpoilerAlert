using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void Start()
    {
        audioManager.playLobbyBGM();
        Time.timeScale = 1f;
    }

    public void SelectGameStage()
    {
        SceneManager.LoadScene("Stage Select");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
