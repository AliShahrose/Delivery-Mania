using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    PlayerController playerController;

    public AudioSource engineAudioSource;
    public AudioSource driftAudioSource;
    public AudioSource collisionAudioSource;

    float enginePitchWanted = 0.5f;
    float driftPitchWanted = 0.5f;


    void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Increase engine volume based on speed
        float velocityMagnitude = playerController.GetVelocityMagnitude();
        float engineVolumeWanted =  velocityMagnitude * 0.2f;
        engineVolumeWanted = Mathf.Clamp(engineVolumeWanted, 0.0f, 1.0f);
        engineAudioSource.volume = Mathf.Lerp(engineAudioSource.volume, engineVolumeWanted, Time.deltaTime * 50.0f);

        // Increase engine pitch based on speed
        enginePitchWanted = velocityMagnitude * 0.2f;
        enginePitchWanted = Mathf.Clamp(enginePitchWanted, 0.0f, 2.0f);
        engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch, enginePitchWanted, Time.deltaTime * 8.0f);

        // Add drifting sound
        if(playerController.isBraking(out bool isBrakingPressed))
        {
            driftAudioSource.volume = Mathf.Lerp(driftAudioSource.volume, 1.0f, Time.deltaTime * 10.0f);
            driftPitchWanted = Mathf.Lerp(driftPitchWanted, 0.2f, Time.deltaTime * 10.0f);
        }
        else
        {
            // Fade away if not drifting
            driftAudioSource.volume = Mathf.Lerp(driftAudioSource.volume, 0.0f, Time.deltaTime * 10.0f);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Add collision sound
        float relativeVelocity = other.relativeVelocity.magnitude;
        float volume = relativeVelocity * 0.5f;

        driftAudioSource.volume = volume;
        driftAudioSource.pitch = Random.Range(0.95f, 1.05f);

        if (!driftAudioSource.isPlaying)
            driftAudioSource.Play();
    }
}
