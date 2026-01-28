using UnityEngine;

public class PathPoint : MonoBehaviour
{
    public bool isOccupied;

    public PathPoint defaultNext;
    public PathPoint[] alternateNext;
}
