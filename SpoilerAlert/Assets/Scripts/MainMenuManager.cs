using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject Credits;
    public LevelLoader levelLoader;
    private AudioManager audioManager;


    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    } 

    public void Start()
    {
        AudioManager.instance.playLobbyBGM();
        Time.timeScale = 1f;
    }

    public void SelectGameStage()
    {
        audioManager.playButtonSFX();
        StartCoroutine(levelLoader.PlayTransition());
    }

    public void CreditsShow()
    {   
        audioManager.playButtonSFX();
        Credits.SetActive(true);
    }
    
    public void CreditsUnShow()
    {
        audioManager.playButtonSFX();
        Credits.SetActive(false);
    }

    public void QuitGame()
    {
        audioManager.playButtonSFX();
        Application.Quit();
    }
}
