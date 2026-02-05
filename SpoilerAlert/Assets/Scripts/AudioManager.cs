using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGM;
    public AudioSource SFX;

    public AudioClip[] bg;
    public AudioClip[] Miscbg;
    public AudioClip[] deathsfx;
    public AudioClip GunshotSFX;
    public AudioClip Reload;
    public AudioClip ButtonSFX;
    private bool gameoverplaying = false;
    private bool isGameOver = false;

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
        BGM.clip = Miscbg[2];
        BGM.Play();
    }

    public void playGameBGM(int waveIndex)
    {
        if(waveIndex >= 0 && waveIndex <= 3)
        {
            BGM.clip = bg[0];
        }else if(waveIndex == 4)
        {
            BGM.clip = bg[1];
        }else if(waveIndex >= 5 && waveIndex <= 6)
        {
            BGM.clip = bg[2];
        }else if(waveIndex == 7)
        {
            BGM.clip = bg[3];
        }else if(waveIndex == 8)
        {
            BGM.clip = bg[2];
        }
        else
        {
            BGM.clip = bg[4];
        }
            
        BGM.Play();
    }

    public void playGameOverBGM(int customersLeft)
    {
        if (gameoverplaying)
        {
            return;
        }

        gameoverplaying = true;
        isGameOver = true;

        BGM.Stop();
        BGM.clip = customersLeft >= 6 ? Miscbg[0] : Miscbg[1];
        
        BGM.time = 0f;
        BGM.Play();  
    }

    public void ResetBGMState()
    {
        gameoverplaying = false;
    }

    public void playHitSFX()
    {   
        if (isGameOver) return;
        int rand = Random.Range(0, deathsfx.Length);
        SFX.PlayOneShot(deathsfx[rand]);
    }

    public void playGunshot()
    {
        if (isGameOver) return;
        SFX.PlayOneShot(GunshotSFX);
    }

    public void playReload()
    {
        if (isGameOver) return;
        SFX.PlayOneShot(Reload);
    }

    public void playButtonSFX()
    {
        SFX.PlayOneShot(ButtonSFX);
    }
}