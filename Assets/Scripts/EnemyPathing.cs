using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    // Variable init
    [SerializeField] WaveConfig waveConfig;

    private List<Transform> waypoints;
    private int currentWaypoint = 0;
    private float enemySpeed = 4f;

    private void Start()
    {
        // Get all data from config file
        waypoints = waveConfig.GetWaypoints();

        transform.position = waypoints[currentWaypoint].transform.position;

    }

    private void Update()
    {
        Move();

    }

    private void Move()
    {
        // Keep enemy moving to last waypoint, if reached, destroy
        if (currentWaypoint < waypoints.Count)
        {
            // Get currentWaypoint position
            var targetPosition = waypoints[currentWaypoint].transform.position;
            var movementSpeed = enemySpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed);

            // Check if enemy reached waypoint to go to next waypoint
            if (transform.position == targetPosition)
            {
                currentWaypoint++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
