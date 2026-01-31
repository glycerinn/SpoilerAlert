using UnityEngine;

[CreateAssetMenu(menuName = "WaveDatas")]
public class WaveDataSO : ScriptableObject
{
    public string waveName;

    public float spawnRate;
    public float spawnStop;     

    public float FastspawnRate;
    public float FastspawnStop; 

    public float StrongspawnRate;
    public float StrongspawnStop; 
    public float waveDuration;  
    

    public GameObject[] enemyPrefabs;
}
