using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireSmokeParticles : MonoBehaviour
{
    PlayerController playerController;
    ParticleSystem smokeParticles;
    ParticleSystem.EmissionModule smokeEmission;

    float emissionRate = 0f;

    void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        smokeParticles = GetComponent<ParticleSystem>();
        smokeEmission = smokeParticles.emission;
        smokeEmission.rateOverTime = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        emissionRate = Mathf.Lerp(emissionRate, 0, Time.deltaTime * 3);
        smokeEmission.rateOverTime = emissionRate;

        if (playerController.isBraking(out bool isBrakingPressed))
        {
            emissionRate = 10;
        }
        else
        {
            emissionRate = 0;
        }

    }
}
