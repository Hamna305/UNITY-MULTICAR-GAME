using UnityEngine;
using TMPro;

public class GameManagerLapSystem : MonoBehaviour
{
    public int totalCheckpoints = 4; // includes finish line as last checkpoint
    public int currentCheckpoint = 0;

    public int totalLaps = 3;
    public int currentLap = 1;

    public TextMeshProUGUI lapText;
    public TextMeshProUGUI checkpointText;

    private void Start()
    {
        UpdateUI();
    }

    public void HitCheckpoint(int checkpointNumber)
    {
        if (checkpointNumber == currentCheckpoint)
        {
            currentCheckpoint++;

            if (currentCheckpoint >= totalCheckpoints)
            {
                currentCheckpoint = 0;
                currentLap++;

                if (currentLap > totalLaps)
                {
                    currentLap = totalLaps;
                    Debug.Log("🏁 Race Finished!");
                }
            }

            UpdateUI();
        }
        else
        {
            Debug.Log("❌ Wrong checkpoint hit. Expected: " + currentCheckpoint + " but got: " + checkpointNumber);
        }
    }

    private void UpdateUI()
    {
        if (lapText != null)
            lapText.text = "Lap: " + currentLap + " / " + totalLaps;

        if (checkpointText != null)
            checkpointText.text = "Checkpoint: " + currentCheckpoint + " / " + totalCheckpoints;
    }
}
