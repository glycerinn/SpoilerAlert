using UnityEngine;

public class PathPoint : MonoBehaviour
{
    public bool isOccupied;
    public bool Used;
    public bool Spoiled;

    public Transform laneExit;

    public PathPoint defaultNext;
    public PathPoint[] alternateNext;
}
