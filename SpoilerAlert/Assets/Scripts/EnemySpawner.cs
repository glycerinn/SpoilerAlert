using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    private WaveDataSO currentWave;
    private float timer;
    private float spawnTimer;
    private int enemiesAlive;
    private bool isSpawning = false;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    public void ConfigureWave(WaveDataSO wave)
    {
        currentWave = wave;
        timer = 0f;
        spawnTimer = 0f;
        enemiesAlive = 0;
        
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
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Update()
    {
        if(!isSpawning||currentWave == null) return;
        timer += Time.deltaTime;

        if(timer >= currentWave.spawnStop
        && timer >= currentWave.FastspawnStop 
        && timer >= currentWave.StrongspawnStop)
        {
            isSpawning = false;
            return;
        }

        spawnTimer += Time.deltaTime;

        if(spawnTimer >= currentWave.spawnRate
        && spawnTimer >= currentWave.StrongspawnRate
        && spawnTimer >= currentWave.FastspawnRate)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private void SpawnEnemy()
    {
        enemiesAlive++;

        int rand = Random.Range(0, LevelManager.main.spawns.Length);
        SpawnPoint spawn = LevelManager.main.spawns[rand];
        GameObject prefabtospawn = currentWave.enemyPrefabs[Random.Range(0, currentWave.enemyPrefabs.Length)];
        GameObject enemy = Instantiate(
            prefabtospawn, 
            spawn.transform.position, 
            Quaternion.identity);
        enemy.GetComponent<EnemyMovement>().Init(spawn.entryPoint);
    }

    public bool IsWaveFinished()
    {
        return !isSpawning && enemiesAlive <= 0;
    }
}
