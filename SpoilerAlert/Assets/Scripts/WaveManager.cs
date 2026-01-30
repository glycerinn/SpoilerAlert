using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] private WaveDataSO[] waves;
    [SerializeField] private EnemySpawner spawner;

    private int currentWaveIndex = -1;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        currentWaveIndex++;

        if(currentWaveIndex >= waves.Length)
        {
            Debug.Log("all done");
            return;
        }

        StartCoroutine(RunWave(waves[currentWaveIndex]));

    }

    private IEnumerator RunWave(WaveDataSO wave)
    {
        while (currentWaveIndex < waves.Length)
        {
            spawner.ConfigureWave(wave);
            spawner.StartSpawn();

            yield return new WaitUntil(() => spawner.IsWaveFinished());

            spawner.StopSpawning();

            yield return new WaitForSeconds(3f);

            StartNextWave();
        }
    }
}
