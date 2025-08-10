using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviourPun
{
    private bool raceFinished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (raceFinished) return;

        if (other.CompareTag("Player") || other.CompareTag("AI"))
        {
            raceFinished = true;

            string winner = other.CompareTag("AI") ? "AI" : PlayerPrefs.GetString("PlayerCarName", "Player");
            PlayerPrefs.SetString("WinnerCar", winner);

            // Load Winner Scene for all players
            PhotonNetwork.LoadLevel("WinnerScene");
        }
    }
}
