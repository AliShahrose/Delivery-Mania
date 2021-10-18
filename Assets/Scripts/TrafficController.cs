using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController : MonoBehaviour
{
    public GameObject waypointsArray;
    public float speed;

    private Waypoints wp;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        wp = waypointsArray.GetComponent<Waypoints>();  
    }

    // Update is called once per frame
    void Update()
    {
        // Move to the new waypoint in the array
        transform.position = Vector2.MoveTowards(transform.position, wp.waypoints[index].position, speed * Time.deltaTime);

        // Change direction depending on where the waypoint is looking
        transform.rotation = wp.waypoints[index].rotation;

        if(Vector2.Distance(transform.position, wp.waypoints[index].position) < 0.1f)
        {
            // Go back to the first waypoint if there's no more in the array
            if(index < wp.waypoints.Length - 1)
            {
                index++;
            }
            else
            {
                index = 0;
            }
        }
    }
}
