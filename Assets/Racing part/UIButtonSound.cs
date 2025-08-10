using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    public AudioSource audioSource; // Drag AudioSource from UIAudioManager
    public AudioClip clickSound;    // Drag your click sound here

    public void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
