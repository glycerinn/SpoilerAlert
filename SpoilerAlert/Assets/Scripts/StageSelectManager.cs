using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    public LevelLoader levelLoader;

    public void BacktoMenu()
    {
        AudioManager.instance.playButtonSFX();
        StartCoroutine(levelLoader.PlayBackTransition());
    }

    public void PlayStage1()
    {
        AudioManager.instance.playButtonSFX();
        SceneManager.LoadScene("SampleScene");
    }
}
