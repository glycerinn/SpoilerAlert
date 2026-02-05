using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public PathPoint[] paths1;
    public PathPoint[] paths2;
    public PathPoint[] paths3;
    public PathPoint[] paths4;
    public SpawnPoint[] spawns;
    public Transform[] exits;
    private AudioManager audioManager;
    

    private void Awake()
    {
        main = this;
    }

    public void Start()
    {
        audioManager.ResetBGMState();
    }

    public Transform GetNearestExit(Vector3 from)
    {
        Transform closest = null;
        float minDist = float.MaxValue;

        foreach (var exit in exits)
        {
            float d = Vector3.Distance(from, exit.position);
            if (d < minDist)
            {
                minDist = d;
                closest = exit;
            }
        }

        return closest;
    }
}
