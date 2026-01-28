using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float Enemiespersecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyFactor = 7.5f;

    private int currentWave = 1;
    private float timeSinceLast;
    private int enemiesAlive;
    private int enemiesLeft;
    private bool isSpawning = false;

    private void Start()
    {
        startWave();
    }

    private void Update()
    {
        if(!isSpawning) return;
        timeSinceLast += Time.deltaTime;

        if(timeSinceLast >= (1f / Enemiespersecond) && enemiesLeft > 0)
        {
            SpawnEnemy();
            enemiesLeft--;
            enemiesAlive++;
            timeSinceLast = 0;
        }
    }

    private void startWave()
    {
        isSpawning = true;
        enemiesLeft = enemiesPerWave();
    }

    private void SpawnEnemy()
    {
        int rand = Random.Range(0, LevelManager.main.spawns.Length);
        SpawnPoint spawn = LevelManager.main.spawns[rand];
        GameObject prefabtospawn = enemyPrefabs[0];
        GameObject enemy = Instantiate(prefabtospawn, spawn.transform.position, Quaternion.identity);
        EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
        movement.Init(spawn.entryPoint);
    }

    private int enemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyFactor));
    }
}
