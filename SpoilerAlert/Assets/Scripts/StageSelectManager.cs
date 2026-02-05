using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    public LevelLoader levelLoader;

    public void BacktoMenu()
    {
        StartCoroutine(levelLoader.PlayBackTransition());
    }

    public void PlayStage1()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
