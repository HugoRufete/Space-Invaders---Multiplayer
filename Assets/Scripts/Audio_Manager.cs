using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioSource backgroundAudioSource;

    public AudioClip StartButtonPressed;
    public AudioClip shipShooting;
    public AudioClip turretShooting;
    public AudioClip shipSpawned;
    public AudioClip alienSpawned;

    public AudioClip gameOverSound;
    private void Start()
    {
        backgroundAudioSource.volume = 0.03f;
        audioSource.volume = 0.4f;
    }
    public void StartButtonSound()
    {
        audioSource.PlayOneShot(StartButtonPressed);

        backgroundAudioSource.volume = 0.04f;
        audioSource.volume = 0.15f;
    }

    public void ShipShooting()
    {
        audioSource.PlayOneShot(shipShooting);
    }

    public void TurretShooting()
    {
        audioSource.PlayOneShot(turretShooting);
    }

    public void ShipSpawned()
    {
        audioSource.PlayOneShot(shipSpawned);
    }

    public void AlienSpawned()
    {
        audioSource.PlayOneShot(alienSpawned);
    }

    public void GameOver()
    {
        audioSource.PlayOneShot(gameOverSound);

    }
}
