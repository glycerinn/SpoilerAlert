using UnityEngine;

[CreateAssetMenu(menuName = "WaveDatas")]
public class WaveDataSO : ScriptableObject
{
    public string waveName;
    public EnemySpawnData[] enemyTypes;
    public float waveDuration;
}
