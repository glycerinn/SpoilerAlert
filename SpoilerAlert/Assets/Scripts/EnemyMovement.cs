using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 2f;

    private PathPoint target;
    private PathPoint claimed;
    private bool stopped;

    public void Init(PathPoint entry)
    {
        target = entry;
    }

    private void Update()
    {
        if (target == null) return;

        if (stopped) return;

        if (Vector2.Distance(transform.position, target.transform.position) <= 0.1f)
        {
            if (!target.isOccupied)
            {
                target.isOccupied = true;
                claimed = target;
                stopped = true;
                rb.linearVelocity = Vector2.zero;
                return;
            }

            PathPoint next = ChooseNext(target);

            if (next == null)
            {
                Destroy(gameObject);
                return;
            }

            target = next;
        }
    }
    
    private PathPoint ChooseNext(PathPoint point)
    {
        if (point.alternateNext != null && point.alternateNext.Length > 0)
        {
            int rand = Random.Range(0, point.alternateNext.Length);
            return point.alternateNext[rand];
        }

        return point.defaultNext;
    }

    private void FixedUpdate()
    {
        if (stopped)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = (target.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    private void OnDestroy()
    {
        if (claimed != null)
        {
            claimed.isOccupied = false;
        }
    }
}
