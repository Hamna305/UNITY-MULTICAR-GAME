using UnityEngine;
using TMPro;

public class GameManagerLapSystem1 : MonoBehaviour
{
    public TextMeshProUGUI checkpointText;

    public void ShowCheckpointNumber(int number)
    {
        if (checkpointText != null)
            checkpointText.text = "Checkpoint: " + number;
    }

    public void RaceFinished()
    {
        if (checkpointText != null)
            checkpointText.text = "🏁 Race Finished!";

        // Optional: Freeze the game
        Time.timeScale = 0f;

        // Optional: Show finish panel, restart button, etc.
        Debug.Log("🏁 Game Over");
    }
}
