using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    private SpriteRenderer sprite;

    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position,
                             transform.position) < .5f) 
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
            if (sprite.flipX == true) 
            {
                sprite.flipX = false;
            } 
            else
            {
                sprite.flipX = true;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position,
                                                 waypoints[currentWaypointIndex].transform.position,
                                                 Time.deltaTime * speed);
    }
}
