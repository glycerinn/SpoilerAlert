using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] private WaveDataSO[] waves;
    [SerializeField] private EnemySpawner spawner;

    private int currentWaveIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(RunWave());
    }

    private IEnumerator RunWave()
    {
        while (currentWaveIndex < waves.Length)
        {
            WaveDataSO wave = waves[currentWaveIndex];

            spawner.ConfigureWave(wave);
            spawner.StartSpawn();

            Debug.Log("wave " + (currentWaveIndex + 1));

            yield return new WaitUntil(() => spawner.IsWaveFinished());

            spawner.StopSpawning();

            yield return new WaitForSeconds(3f);

            currentWaveIndex++;
        }
    }
}
