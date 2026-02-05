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
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private ScoreManager scoreManager;

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

    public void playGameBGM()
    {
        if(waveManager.currentWaveIndex >= 0 && waveManager.currentWaveIndex <= 3)
        {
            BGM.clip = bg[0];
        }else if(waveManager.currentWaveIndex == 4)
        {
            BGM.clip = bg[1];
        }else if(waveManager.currentWaveIndex >= 5 && waveManager.currentWaveIndex <= 6)
        {
            BGM.clip = bg[2];
        }else if(waveManager.currentWaveIndex == 7)
        {
            BGM.clip = bg[3];
        }else if(waveManager.currentWaveIndex == 8)
        {
            BGM.clip = bg[2];
        }
        else
        {
            BGM.clip = bg[4];
        }
            
        BGM.Play();
    }

    public void playGameOverBGM()
    {
        if (gameoverplaying)
        {
            return;
        }

        gameoverplaying = true;
        isGameOver = true;

        BGM.Stop();
        if(scoreManager.customersLeft >= 6)
        {
            BGM.clip = Miscbg[0];
        }
        else
        {
            BGM.clip = Miscbg[1];
        }
        
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