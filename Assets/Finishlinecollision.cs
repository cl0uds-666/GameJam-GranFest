using UnityEngine;
using System.Collections;

public class RandomSFXOnCollision : MonoBehaviour
{
    [SerializeField] private AudioClip[] sfxClips; // Assign your 3 SFX clips in the Inspector
    [SerializeField] private AudioSource audioSource; // Assign an AudioSource component in the Inspector

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayRandomSFX();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayRandomSFX();
    }

    private void PlayRandomSFX()
    {
        if (sfxClips == null || sfxClips.Length == 0 || audioSource == null)
        {
            Debug.LogWarning("SFX clips or AudioSource not properly set up!");
            return;
        }

        // Get a random index between 0 and the number of clips
        int randomIndex = Random.Range(0, sfxClips.Length);

        // Play the randomly selected clip
        audioSource.PlayOneShot(sfxClips[randomIndex]);
    }
}
