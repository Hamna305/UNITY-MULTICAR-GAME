using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointNumber;
    public GameManagerLapSystem1 gameManager;
    public AudioSource checkpointSound; // 🎵

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("✅ Triggered checkpoint " + checkpointNumber);

            // Play the checkpoint sound if assigned
            if (checkpointSound != null)
                checkpointSound.Play();

            gameManager.ShowCheckpointNumber(checkpointNumber);
        }
    }
}
