using UnityEngine;
using UnityEngine.UI;

public class StopMusicButton : MonoBehaviour
{
    public AudioSource soundSource;     // Drag your AudioSource here
    public Image buttonImage;           // Drag your button's Image component
    public Sprite soundOnSprite;        // Speaker with sound
    public Sprite soundOffSprite;       // Speaker muted

    private bool isPlaying = true;      // Music starts playing

    // Toggle music on/off
    public void ToggleMusic()
    {
        if (soundSource == null)
        {
            Debug.LogError("SoundSource not assigned!");
            return;
        }

        if (isPlaying)
        {
            soundSource.Pause();                   // Pause the music
            if (buttonImage != null) 
                buttonImage.sprite = soundOffSprite;  // Switch to muted icon
            Debug.Log("🎵 Music paused!");
        }
        else
        {
            soundSource.Play();                    // Resume the music
            if (buttonImage != null) 
                buttonImage.sprite = soundOnSprite;   // Switch to sound icon
            Debug.Log("🎵 Music resumed!");
        }

        isPlaying = !isPlaying;
    }
}
