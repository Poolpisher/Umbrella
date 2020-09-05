using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EndPickup2 : MonoBehaviour
{
        [SerializeField]
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        audioSource.Stop();
        audioSource.Play();
        GetComponent<Collider>().enabled = false;
    }
}