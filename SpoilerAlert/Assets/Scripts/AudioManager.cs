using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGM;
    public AudioSource SFX;

    public AudioClip[] bg;
    public AudioClip[] deathsfx;
    public AudioClip GunshotSFX;
    public AudioClip Reload;
    public AudioClip ButtonSFX;
    private bool gameoverplaying = false;

    public static AudioManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    
    public void playLobbyBGM()
    {
        BGM.clip = bg[0];
        BGM.Play();
    }

    public void playGameBGM()
    {
        BGM.clip = bg[1];
        BGM.Play();
    }

    public void playGameOverBGM()
    {
        if (gameoverplaying)
        {
            return;
        }

        gameoverplaying = true;

        BGM.Stop();
        BGM.clip = bg[2];
        BGM.time = 0f;
        BGM.Play();  
    }

    public void ResetBGMState()
    {
        gameoverplaying = false;
    }

    public void playSFX(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }

    public void playHitSFX()
    {
        int rand = Random.Range(0, deathsfx.Length);
        SFX.PlayOneShot(deathsfx[rand]);
    }

    public void playGunshot()
    {
        SFX.PlayOneShot(GunshotSFX);
    }

    public void playReload()
    {
        SFX.PlayOneShot(Reload);
    }

    public void playButtonSFX()
    {
        SFX.PlayOneShot(ButtonSFX);
    }
}