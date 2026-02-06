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
    private bool waveFinishedSignaled;

    private float[] spawnTimers;


    public void ConfigureWave(WaveDataSO wave)
    {
        enemiesAlive = 0;
        
        EnemyMovement[] leftovers =
            Object.FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);

        foreach (var enemy in leftovers)
        {
            Destroy(enemy.gameObject);
        }

        currentWave = wave;
        timer = 0f;
        enemiesAlive = 0;

        spawnTimers = new float[wave.enemyTypes.Length];

        waveActive = true;
        isSpawning = true;
        waveTimedOut = false;
        waveFinishedSignaled = false;
    }

    public void StartSpawn()
    {
        isSpawning = true;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }


    private void Update()
    {
        if(currentWave == null) return;
        timer += Time.deltaTime;

        if (!waveTimedOut && timer >= currentWave.waveDuration)
        {
            Debug.Log("Wave timed out forcing enemies to exit");
            waveTimedOut = true;
            isSpawning = false;
            waveActive = false;
            EndWaveByTimeout();
        }

        if (isSpawning)
        {
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

            if (!waveTimedOut && AllSpawningFinished() && enemiesAlive <= 0)
            {
                Debug.Log("Wave cleared early");
                waveActive = false;
                return;
            }

            if (!isSpawning && enemiesAlive <= 0)
            {
                Debug.Log("Wave fully cleared");
                waveActive = false;
            }
        }


        if (!isSpawning && enemiesAlive <= 0)
        {
            Debug.Log("Wave fully cleared");
            waveActive = false;
        }
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
        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        movement.Init(spawn.entryPoint);
        movement.SetSpawner(this);
        enemy.GetComponent<EnemyMovement>().Init(spawn.entryPoint);
    }

    public bool IsWaveFinished()
    {
        if (waveFinishedSignaled)
            return true;

        if (!waveActive && enemiesAlive <= 0)
        {
            waveFinishedSignaled = true;
            Debug.Log("Wave finished (latched)");
            return true;
        }

        return false;
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

    public void NotifyEnemyDestroyed()
    {
        enemiesAlive--;

        if (enemiesAlive < 0)
        {
            Debug.LogError("enemiesAlive went negative! This should never happen.");
            enemiesAlive = 0;
        }

        Debug.Log("Enemy destroyed, alive: " + enemiesAlive);
    }

    


}
