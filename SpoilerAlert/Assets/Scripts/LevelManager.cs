using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public PathPoint[] paths1;
    public PathPoint[] paths2;
    public PathPoint[] paths3;
    public PathPoint[] paths4;
    public SpawnPoint[] spawns;
    

    private void Awake()
    {
        main = this;

    }
}
