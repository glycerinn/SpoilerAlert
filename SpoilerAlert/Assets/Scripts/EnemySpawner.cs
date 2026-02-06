using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    private WaveDataSO currentWave;
    private float timer;
    private int enemiesAlive;
    
    private bool isSpawning = false;
    private bool waveActive;
    private bool waveTimedOut;

    private float[] spawnTimers;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    public void ConfigureWave(WaveDataSO wave)
    {
        currentWave = wave;
        timer = 0f;
        enemiesAlive = 0;

        spawnTimers = new float[wave.enemyTypes.Length];

        waveActive = true;
        isSpawning = true;
        waveTimedOut = false;
    }

    public void StartSpawn()
    {
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    private void Awake()
    {
        onEnemyDestroy.RemoveAllListeners();
        onEnemyDestroy.AddListener(EnemyDestroyed);
        Debug.Log("EnemySpawner Awake: " + gameObject.name);
        Debug.Log("EnemySpawner subscribed to onEnemyDestroy");
    }

    private void Update()
    {
        if(currentWave == null) return;
        timer += Time.deltaTime;

        if (isSpawning && AllSpawningFinished() && enemiesAlive <= 0)
        {
            Debug.Log("Wave cleared early");
            isSpawning = false;
            waveActive = false;
            return;
        }

        if (!waveTimedOut && timer >= currentWave.waveDuration)
        {
            waveTimedOut = true;
            EndWaveByTimeout();
            isSpawning = false;
            return;
        }

        for (int i = 0; i < currentWave.enemyTypes.Length; i++)
        {
            EnemySpawnData type = currentWave.enemyTypes[i];

            if (timer >= type.stopTime)
                continue;

            spawnTimers[i] += Time.deltaTime;

            if (spawnTimers[i] >= type.spawnRate)
            {
                SpawnEnemy(type);
                spawnTimers[i] = 0f;
            }
        }


        if (!isSpawning && enemiesAlive <= 0)
        {
            Debug.Log("Wave fully cleared");
            waveActive = false;
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
        Debug.Log("Enemy destroyed, alive: " + enemiesAlive);

    }

    private void SpawnEnemy(EnemySpawnData type)
    {
        enemiesAlive++;

        Debug.Log("Enemy spawned, alive: " + enemiesAlive);

        SpawnPoint spawn = LevelManager.main.spawns[
        Random.Range(0, LevelManager.main.spawns.Length)
        ];

        GameObject prefab = type.prefabs[
            Random.Range(0, type.prefabs.Length)
        ];

        GameObject enemy = Instantiate(
            prefab,
            spawn.transform.position,
            Quaternion.identity
        );

        enemy.GetComponent<EnemyMovement>().Init(spawn.entryPoint);
    }

    public bool IsWaveFinished()
    {
        return !waveActive && enemiesAlive <= 0;
    }

    private void EndWaveByTimeout()
    {
        isSpawning = false;

        EnemyMovement[] enemies =
            Object.FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);

        foreach (EnemyMovement enemy in enemies)
        {
            enemy.ForceExit();
        }
    }

    private bool AllSpawningFinished()
    {
        for (int i = 0; i < currentWave.enemyTypes.Length; i++)
        {
            if (timer < currentWave.enemyTypes[i].stopTime)
                return false;
        }
        return true;
    }


}
