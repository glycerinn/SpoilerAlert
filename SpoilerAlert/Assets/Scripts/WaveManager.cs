using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] private CustomerManager customerManager;
    [SerializeField] private GameOverScript gameOverScript;
    [SerializeField] private WaveDataSO[] waves;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private WaveUI waveUI;

    public int currentWaveIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(RunWave());
    }

    private void Update()
    {
        if(currentWaveIndex == waves.Length && spawner.IsWaveFinished())
        {
            AudioManager.instance.playGameOverBGM(customerManager.customersRemaining);
            gameOverScript.SetUp();
            Time.timeScale = 0f;
        }
    }

    private IEnumerator RunWave()
    {
        while (currentWaveIndex < waves.Length)
        {
            AudioManager.instance.playGameBGM(currentWaveIndex);

            WaveDataSO wave = waves[currentWaveIndex];

            waveUI.showWave(currentWaveIndex + 1);

            Debug.Log("Starting wave " + currentWaveIndex);

            spawner.ConfigureWave(wave);
            spawner.StartSpawn();

            Debug.Log("wave " + (currentWaveIndex + 1));

            yield return new WaitUntil(() => spawner.IsWaveFinished());

            Debug.Log("Wave " + currentWaveIndex + " finished");

            spawner.StopSpawning();

            yield return new WaitForSeconds(3f);

            currentWaveIndex++;
        }

        
        
    }
}
