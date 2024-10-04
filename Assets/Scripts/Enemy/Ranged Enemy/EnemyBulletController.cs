using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyBulletController : MonoBehaviour
{
    public float moveSpeed;
    public float nextWaypointDistance = 3f;
    [SerializeField] private PlayerHealthManager healthManager;

    Transform target;
    GameObject playerInstance;
    Path path;
    int currentWaypoint;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        playerInstance = GameObject.FindGameObjectWithTag("Player");
        target = playerInstance.transform;
        InvokeRepeating("UpdatePath", 0f, .5f);
        Invoke("DestroyThis", 5f);
    }

    private void UpdatePath ()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete (Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        Vector2 force = direction * moveSpeed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
    
    private void DestroyThis ()
    {
        CancelInvoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            healthManager.ApplyDamage(1, collision.gameObject);
            DestroyThis();
        }
        else
        {
            DestroyThis();
        }

    }
}
