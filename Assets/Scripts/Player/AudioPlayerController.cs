using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private MovementController movement;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<MovementController>();
    }

    private void FixedUpdate()
    {
        if (movement.isRuning)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
