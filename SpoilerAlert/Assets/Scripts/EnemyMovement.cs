using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private PathPoint target;
    private PathPoint claimed;
    private bool stopped;
    public EnemyBehaviour enemyBehaviour;
    public Vector3 exitPoint;

    private bool isLeaving = false;
    private Transform exitTarget;

    private enum ExitPhase { None, Vertical, Horizontal }
    private ExitPhase exitPhase;
    private bool deathNotified = false;
    private EnemySpawner spawner;

    public void Init(PathPoint entry)
    {
        target = entry;
    }

    public void SetSpawner(EnemySpawner spawner)
    {
        this.spawner = spawner;
    }

    private void Update()
    {
        if (target == null) return;

        if (stopped) {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if ((transform.position - target.transform.position).sqrMagnitude <= 0.05f)
        {
            if (target.isSeat && !target.isOccupied)
            {
                target.isOccupied = true;
                claimed = target;
                stopped = true;
                rb.linearVelocity = Vector2.zero;

                transform.position = target.transform.position + new Vector3(0f, 0.1f, 0f);

                enemyBehaviour.showSpoilerBar(claimed);

                return;
            }
            
            if (!HasFreeSeatAhead(target))
            {
                ForceExit();
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
        if (isLeaving)
        {
            Vector2 dir;

            if (exitPhase == ExitPhase.Vertical)
            {
                dir = new Vector2(0f, Mathf.Sign(exitTarget.position.y - transform.position.y));

                if (Mathf.Abs(transform.position.y - exitTarget.position.y) < 0.05f)
                    exitPhase = ExitPhase.Horizontal;
            }
            else
            {
                dir = new Vector2(Mathf.Sign(exitTarget.position.x - transform.position.x), 0f);

                if (Mathf.Abs(transform.position.x - exitTarget.position.x) < 0.05f)
                {
                    rb.linearVelocity = Vector2.zero;
                    Destroy(gameObject);
                    return;
                }
            }

            rb.linearVelocity = dir * 5f;
            enemyBehaviour.UpdateAnimation(dir);
            return;
        }

        if (stopped)
        {
            rb.linearVelocity = Vector2.zero;
            enemyBehaviour.UpdateAnimation(Vector2.zero);
            return;
        }

        if (target == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = (target.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * enemyBehaviour.EnemySO.EnemySpeed;
        enemyBehaviour.UpdateAnimation(direction);
    }

    private void OnDestroy()
    {
        if (deathNotified) return;
        deathNotified = true;

        if (spawner != null)
            spawner.NotifyEnemyDestroyed();

        if (claimed != null)
            claimed.isOccupied = false;

        Debug.Log("Enemy destroyed (counted once): " + gameObject.name);
    }

    public void ForceExit()
    {
        if (isLeaving) return;

        isLeaving = true;
        stopped = false;
        target = null;

        if (claimed != null)
            claimed.isOccupied = false;

         exitTarget = claimed?.laneExit;

        if (exitTarget == null)
            exitTarget = LevelManager.main.GetNearestExit(transform.position);

        if (exitTarget == null)
        {
            Debug.LogWarning("No exit found, destroying enemy");
            Destroy(gameObject);
            return;
        }
    }

    private bool HasFreeSeatAhead(PathPoint start)
    {
        HashSet<PathPoint> visited = new HashSet<PathPoint>();
        return CheckSeatRecursive(start, visited);
    }

    private bool CheckSeatRecursive(PathPoint point, HashSet<PathPoint> visited)
    {
        if (point == null)
            return false;

        if (visited.Contains(point))
            return false;

        visited.Add(point);

        if (point.isSeat && !point.isOccupied)
            return true;

        if (CheckSeatRecursive(point.defaultNext, visited))
            return true;

        if (point.alternateNext != null)
        {
            foreach (var alt in point.alternateNext)
            {
                if (CheckSeatRecursive(alt, visited))
                    return true;
            }
        }

        return false;
    }
}
