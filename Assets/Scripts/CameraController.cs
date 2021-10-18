using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float xPos = 0.0f;
    public float yPos = 0.0f;
    public float zPos = -10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LateUpdate()
    {
        // Follow the player's position with some offset
        transform.position = new Vector3(target.position.x + xPos, target.position.y + yPos, zPos);
        
    }
}
