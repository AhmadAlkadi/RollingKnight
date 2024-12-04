using UnityEngine;
using Pathfinding;

public class CrowAI : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Transform enemy;

    private Path path;

    private int currentWaypoint = 0;

    private bool reachedEndOfPath = false;
    private Seeker seeker;

    private Rigidbody2D rb;

    void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponentInChildren<Rigidbody2D>();

        InvokeRepeating("UpdatePath",0f,0.5f);
        
    }

    void UpdatePath() {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p) {
        if(!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate() {
        if(path == null) {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count) {
            reachedEndOfPath = true;
            
            return;
        } else {
            //Debug.Log(currentWaypoint);
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        // Detect if the player is higher and the wizard is blocked
        if (target.position.y > rb.position.y) {
            direction.y = 1f; // Prioritize upward movement
        }
        
        Vector2 force = direction * speed * Time.deltaTime;
        
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance) {
            currentWaypoint++;
        }

        //Debug.Log($"{gameObject.name} velocity after force: {rb.linearVelocity}");

        if(rb.linearVelocity.x >= 0.01f) {
            enemy.localScale = new Vector3(1f,1f,1f);
        } else if(rb.linearVelocity.x <= -0.01f) {
            enemy.localScale = new Vector3(-1f,1f,1f);
        }
    }
}
