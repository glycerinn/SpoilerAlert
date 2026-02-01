using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    public void BacktoMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void PlayStage1()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
