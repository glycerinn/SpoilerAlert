using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    private AudioManager audioManager;
    public GameObject GameOverPanel;
    [SerializeField] private ScoreManager scoreManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void SetUp()
    {
        GameOverPanel.SetActive(true);
    }

    public void GoBack()
    {
        audioManager.playButtonSFX();
        SceneManager.LoadScene("Main Menu");
    }
}
