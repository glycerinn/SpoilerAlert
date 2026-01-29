using System.Collections;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    private PathPoint seat;
    private bool leaving;

    [SerializeField] private float moveTime = 0.8f;

    public void AssignSeat(PathPoint seatPoint)
    {
        seat = seatPoint;
    }

    private void Update()
    {
        if (seat == null)
        return;

        if (!leaving && seat.Spoiled)
        {
            leaving = true;
            StartCoroutine(LeaveSequence());
        }
    }

    private IEnumerator LeaveSequence()
    {
        yield return MoveTo(seat.transform.position);

        if (seat.laneExit != null)
            yield return MoveTo(seat.laneExit.position);

        seat.Used = false;
        Destroy(gameObject);
    }

     private IEnumerator MoveTo(Vector3 target)
    {
        Vector3 start = transform.position;
        float t = 0f;

        while (t < moveTime)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, target, t / moveTime);
            yield return null;
        }

        transform.position = target;
    }
}
