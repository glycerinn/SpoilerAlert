using System.Collections;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    private PathPoint seat;
    private bool leaving;
    private bool hasLeft = false;

    [SerializeField] private Animator animator;
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
            animator.SetBool("Sit", false);
            StartCoroutine(LeaveSequence());
        }
    }

    private IEnumerator LeaveSequence()
    {
        if (hasLeft) yield break;
            hasLeft = true;

        yield return MoveTo(seat.transform.position);

        if (seat.laneExit != null)
            yield return MoveTo(seat.laneExit.position);

        seat.Used = false;

        CustomerManager.Instance.CustomerLeft();

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
