using UnityEngine;
using Cinemachine;
using System.Collections;

public class AssignCameraTarget : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;

    IEnumerator Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();

        // Wait a short moment for the car to spawn
        yield return new WaitForSeconds(0.1f);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Transform followPoint = player.transform.Find("Follow Point");
            if (followPoint != null)
            {
                vCam.Follow = followPoint;
                vCam.LookAt = followPoint;
            }
            else
            {
                Debug.LogError("❌ FollowPoint not found on the player car!");
            }
        }
        else
        {
            Debug.LogError("❌ Player car not found in scene!");
        }
    }
}
