using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        Debug.Log("Connecting to Photon...");
        PhotonNetwork.ConnectUsingSettings(); // Connect to Photon servers
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master! Joining Lobby...");
        PhotonNetwork.JoinLobby(); // Join default lobby
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby! Creating/Joining Room...");
        PhotonNetwork.JoinOrCreateRoom("TestRoom", new RoomOptions() { MaxPlayers = 5 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("✅ Successfully joined room: " + PhotonNetwork.CurrentRoom.Name);
    }
}
