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
    
    void Start()
    {
        BGM.clip = bg[1];
        BGM.Play();
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