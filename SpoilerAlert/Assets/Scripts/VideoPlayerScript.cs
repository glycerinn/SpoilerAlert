using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public LevelLoader levelLoader;
    public GameObject SkipButton;

    void Start()
    {
        SkipButton.SetActive(false);
        videoPlayer.loopPointReached += LoadNextScene;
    }

    void Update()
    {
        StartCoroutine(ShowSkipButton());
    }

    public void LoadNextScene(VideoPlayer videoPlayer)
    {
        StartCoroutine(levelLoader.PlayBackTransition());
    }

    public IEnumerator ShowSkipButton()
    {
        yield return new WaitForSeconds(3f);
        SkipButton.SetActive(true);
    }

    public void SkipVideo()
    {
        StartCoroutine(levelLoader.PlayBackTransition());
    }
}
