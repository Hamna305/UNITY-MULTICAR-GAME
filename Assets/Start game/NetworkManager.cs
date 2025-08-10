using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public string gameSceneName = "SampleScene"; // your racing scene

    void Start()
    {
        Debug.Log("Connecting to Photon...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server!");
        PhotonNetwork.JoinLobby(); // join default lobby
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby!");
    }

    public void CreateOrJoinRoom()
    {
        // Called when clicking the Start Game button
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4; // Change for more players
        PhotonNetwork.JoinOrCreateRoom("RaceRoom", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room! Loading game scene...");
        PhotonNetwork.LoadLevel(gameSceneName); // load racing scene for all players
    }
}
