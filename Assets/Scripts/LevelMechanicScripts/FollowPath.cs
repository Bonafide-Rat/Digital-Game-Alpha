using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowPath : MonoBehaviour
{

    // Array of waypoints to walk from one to the next one
    [SerializeField]
    private Transform[] waypoints;

    // Walk speed that can be set in Inspector
    [SerializeField]
    private float moveSpeed = 2f;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    private void Start()
    {
        // Set position of Enemy as position of the first waypoint
        transform.position = waypoints[waypointIndex].transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Move Enemy
        Move();
        Flip();
    }

    // Method that actually make Enemy walk
    private void Move()
    {
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {
            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = 
                Vector2.MoveTowards(transform.position, waypoints[waypointIndex + 1].transform.position, moveSpeed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (Vector2.Distance(transform.position, waypoints[waypointIndex + 1].position) < 0.5f)
            {
                // Debug.Log("Pos reached.");
                waypointIndex += 1;
            }
        }
    }

    public void ResetToLastWaypoint()
    {
        transform.position = waypoints[waypointIndex].position; // Reset position to the last visited waypoint
    }

    private void Flip()
    {
        if (waypointIndex < waypoints.Length - 1)
        {
            // Determine if the target waypoint is to the left or right of the current position
            bool shouldFlip = (waypoints[waypointIndex + 1].position.x < transform.position.x && !spriteRenderer.flipX) ||
                              (waypoints[waypointIndex + 1].position.x > transform.position.x && spriteRenderer.flipX);
            if (shouldFlip)
            {
                // Flip the sprite by toggling the flipX property
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
    }
}