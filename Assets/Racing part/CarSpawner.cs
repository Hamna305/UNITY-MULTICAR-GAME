using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class CarSpawner : MonoBehaviourPunCallbacks
{
    [Header("Spawn Settings")]
    public Transform[] spawnPoints;       // Assign empty GameObjects in the Inspector
    public GameObject[] carPrefabs;       // Assign car prefabs (must be in Resources folder)

    [Header("Camera Settings")]
    public CinemachineVirtualCamera vCam; // Drag your Cinemachine camera here

    private void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Not connected to Photon! Cannot spawn car.");
            return;
        }

        StartCoroutine(SpawnCarWhenReady());
    }

    private System.Collections.IEnumerator SpawnCarWhenReady()
    {
        // Wait until room and player are ready
        while (PhotonNetwork.CurrentRoom == null || PhotonNetwork.LocalPlayer == null)
        {
            yield return null;
        }

        // 1. Get the car the player selected
        int selectedCar = PlayerPrefs.GetInt("SelectedCarIndex", 0);

        // 2. Pick spawn point based on ActorNumber (unique per player)
        int spawnIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        spawnIndex = Mathf.Clamp(spawnIndex, 0, spawnPoints.Length - 1);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // 3. Spawn the car over network
        GameObject car = PhotonNetwork.Instantiate(
            carPrefabs[selectedCar].name,
            spawnPoint.position,
            spawnPoint.rotation
        );

        // 4. Tag car as Player for local scripts
        car.tag = "Player";

        // 5. Setup Cinemachine camera to follow ONLY the local player car
        PhotonView view = car.GetComponent<PhotonView>();
        if (view != null && view.IsMine)
        {
            Transform followPoint = car.transform.Find("Follow Point");

            if (vCam != null && followPoint != null)
            {
                vCam.Follow = followPoint;
                vCam.LookAt = followPoint;
                Debug.Log("🎥 Camera following: " + car.name);
            }
            else
            {
                Debug.LogWarning("FollowPoint or Camera missing on local car!");
            }
        }

        Debug.Log("🚗 Spawned car: " + car.name + " at " + spawnPoint.name);
    }
}
