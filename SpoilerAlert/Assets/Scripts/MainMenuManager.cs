using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private AudioManager audioManager;
    public GameObject Credits;
    public LevelLoader levelLoader;

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
        StartCoroutine(levelLoader.PlayTransition());
    }

    public void CreditsShow()
    {
        Credits.SetActive(true);
    }
    
    public void CreditsUnShow()
    {
        Credits.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
