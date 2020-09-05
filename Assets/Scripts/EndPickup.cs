using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EndPickup : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private GameObject finishScreen;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.Rotate(transform.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.Stop();
        audioSource.Play();
        Instantiate(finishScreen);
        GetComponent<Collider>().enabled = false;
    }
}