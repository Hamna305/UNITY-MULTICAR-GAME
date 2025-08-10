using System.Collections;
using UnityEngine;
using Photon.Pun;  // ✅ Needed for Photon features

public class gamemanager1 : MonoBehaviour
{
    public GameObject needle;
    private float startPosition = 220f, endPosition = -49f;
    private float desiredPosition;
    private float vehicleSpeed;

    private CarController RR;

    void Start()
    {
        // Only start checking for local player car
        StartCoroutine(FindLocalPlayerCar());
    }

    IEnumerator FindLocalPlayerCar()
    {
        while (RR == null)
        {
            // Find all objects tagged as Player
            GameObject[] allCars = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject car in allCars)
            {
                PhotonView view = car.GetComponent<PhotonView>();

                // ✅ We only take the local player's car
                if (view != null && view.IsMine)
                {
                    RR = car.GetComponent<CarController>();
                    Debug.Log("✅ Local player car found: " + car.name);
                    break;
                }
            }

            if (RR == null)
            {
                Debug.LogWarning("⏳ Local car not spawned yet... waiting");
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    void Update()
    {
        if (RR == null) return;

        vehicleSpeed = RR.KPH;
        updateNeedle();
    }

    private void updateNeedle()
    {
        desiredPosition = startPosition - endPosition;
        float temp = vehicleSpeed / 180f;
        needle.transform.eulerAngles = new Vector3(0, 0, startPosition - temp * desiredPosition);
    }
}
