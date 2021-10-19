using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    
    public GameObject waypointsArray;
    public Vector3 directionVector;
    public float speed;

    private Animator animator;

    private Waypoints wp;
    private int index;
    private string direction;

    // Start is called before the first frame update
    void Start()
    {
        wp = waypointsArray.GetComponent<Waypoints>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // Move to the new waypoint in the array
        transform.position = Vector2.MoveTowards(transform.position, wp.waypoints[index].position, speed * Time.deltaTime);

        // Change direction depending on where the waypoint is looking

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
            direction = wp.waypoints[index].name;
            changeDirection(direction);
        }
    }

    void changeDirection(string direction)
    {
        switch (direction)
        {
            case "North":
                directionVector = Vector3.up;
                break;

            case "South":
                directionVector = Vector3.down;
                break;

            case "East":
                directionVector = Vector3.right;
                break;

            case "West":
                directionVector = Vector3.left;
                break;

            default:
                break;
        }
        
        updateAnimation();
    }

    void updateAnimation()
    {
        animator.SetFloat("MoveX", directionVector.x);
        animator.SetFloat("MoveY", directionVector.y);
    }
}
