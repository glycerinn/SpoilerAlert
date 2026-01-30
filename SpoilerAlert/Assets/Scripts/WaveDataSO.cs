using UnityEngine;

[CreateAssetMenu(menuName = "WaveDatas")]
public class WaveDataSO : ScriptableObject
{
    public string waveName;

    public float spawnRate;     
    public float waveDuration;  
    public float spawnStop;

    public GameObject[] enemyPrefabs;
}
