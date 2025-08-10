using UnityEngine;
using UnityEngine.UI;

public class CarSelector : MonoBehaviour
{
    public GameObject[] carPrefabs; // assign in Inspector
    private int currentCarIndex = 0;
    private GameObject currentCarInstance;

    void Start()
    {
        SpawnCar(currentCarIndex);
    }

    public void NextCar()
    {
        currentCarIndex = (currentCarIndex + 1) % carPrefabs.Length;
        SpawnCar(currentCarIndex);
    }

    public void PreviousCar()
    {
        currentCarIndex--;
        if (currentCarIndex < 0)
            currentCarIndex = carPrefabs.Length - 1;

        SpawnCar(currentCarIndex);
    }

    void SpawnCar(int index)
    {
        if (currentCarInstance != null)
            Destroy(currentCarInstance);

        currentCarInstance = Instantiate(carPrefabs[index], new Vector3(0, 0, 0), Quaternion.identity);
        currentCarInstance.transform.rotation = Quaternion.Euler(0, 90, 0); // rotate to face camera
    }

   public void ConfirmSelection()
{
    PlayerPrefs.SetInt("SelectedCarIndex", currentCarIndex);
    PlayerPrefs.SetString("PlayerCarName", carPrefabs[currentCarIndex].name); // Save car name
    PlayerPrefs.SetString("WinnerCar", ""); // Reset winner
    UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene"); 
}

}
