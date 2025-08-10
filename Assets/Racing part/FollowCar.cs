using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowCar : MonoBehaviour
{
    private Transform target;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float followSpeed = 10f;

    void Start()
    {
        // Only run follow logic in the racing scene
        if (SceneManager.GetActiveScene().name == "SampleScene") // Replace with your racing scene name
        {
            StartCoroutine(FindCarAfterDelay());
        }
    }

    IEnumerator FindCarAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);

        GameObject playerCar = GameObject.FindWithTag("Player");
        if (playerCar != null)
        {
            target = playerCar.transform;
        }
        else
        {
            Debug.LogError("Player car not found! Make sure it's tagged 'Player'");
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            transform.LookAt(target);
        }
    }
}
