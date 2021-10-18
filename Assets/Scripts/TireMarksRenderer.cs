using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireMarksRenderer : MonoBehaviour
{
    PlayerController playerController;
    TrailRenderer trailRenderer;

    void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        trailRenderer = GetComponent<TrailRenderer>();

        trailRenderer.emitting = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isBraking(out bool isBrakingPressed))
        {
            trailRenderer.emitting = true;
        }
        else
        {
            trailRenderer.emitting = false;
        }
    }
}
