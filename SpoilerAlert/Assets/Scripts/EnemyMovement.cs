using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private PathPoint target;
    private PathPoint claimed;
    private bool stopped;
    public EnemyBehaviour enemyBehaviour;

    private bool isLeaving = false;
    private Transform exitTarget;


    public void Init(PathPoint entry)
    {
        target = entry;
    }

    private void Update()
    {
        if (isLeaving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                exitTarget.position,
                5f * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, exitTarget.position) < 0.1f)
            {
                Destroy(gameObject);
            }
            return;
        }

        if (target == null) return;

        if (stopped) return;

        if ((transform.position - target.transform.position).sqrMagnitude <= 0.05f)
        {
            if (target.isSeat && !target.isOccupied)
            {
                target.isOccupied = true;
                claimed = target;
                stopped = true;
                transform.position = target.transform.position;
                rb.linearVelocity = Vector2.zero;

                enemyBehaviour.showSpoilerBar(claimed);

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

        if (isLeaving)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = (target.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * enemyBehaviour.EnemySO.EnemySpeed;
    }

    private void OnDestroy()
    {
        Debug.Log("Enemy OnDestroy fired: " + gameObject.name);
        EnemySpawner.onEnemyDestroy.Invoke();
        if (claimed != null)
        {
            claimed.isOccupied = false;
        }
    }

    public void ForceExit()
    {
        if (isLeaving) return;

        isLeaving = true;
        stopped = false;
        target = null;

        if (claimed != null)
        {
            claimed.isOccupied = false;
        }

        if (claimed != null && claimed.laneExit != null)
        {
            exitTarget = claimed.laneExit;
        }
        else
        {
            Debug.LogWarning("Enemy has no lane exit, destroying.");
            Destroy(gameObject);
        }
    }

}
