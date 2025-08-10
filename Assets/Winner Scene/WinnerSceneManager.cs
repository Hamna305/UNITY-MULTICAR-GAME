using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinnerSceneManager : MonoBehaviour
{
    [Header("Winner Setup")]
    public TextMeshProUGUI winnerText;    // TMP UI text for displaying the winner

    void Start()
    {
        // 1. Get winner name from PlayerPrefs
        string winnerName = PlayerPrefs.GetString("WinnerCar", "Unknown");

        // 2. Check if winner is AI or Player
        if (winnerName == "AI")
        {
            winnerText.text = "AI CAR WINS!";
        }
        else if (winnerName == "Unknown")
        {
            winnerText.text = "No Winner Detected!";
            Debug.LogWarning("No winner name was set in PlayerPrefs!");
        }
        else
        {
            winnerText.text = winnerName + " WINS!";
        }
    }

    // Called by button to return to menu
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("start"); // Replace with your actual start menu scene name
    }
}
