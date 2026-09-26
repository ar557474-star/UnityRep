using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    [SerializeField] GameObject[] cars;
    int currentCarIndex;
    void Start()
    {
        Time.timeScale = 1f;
        ShowCar(currentCarIndex);
    }

    // Update is called once per frame
    void Update()
    {
        //ShowCar(currentCarIndex);
    }
    public void NextCar()
    {
        currentCarIndex++;
        if(currentCarIndex == cars.Length) currentCarIndex = 0;
        ShowCar(currentCarIndex);
    }
    public void PreviousCar()
    {
        currentCarIndex--;

        if (currentCarIndex < 0) currentCarIndex = cars.Length -1;
        ShowCar(currentCarIndex);
    }
    void ShowCar(int index)
    {
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(i == currentCarIndex);
        }
    }

    public void SelectCar()
    {
        //SceneManager.LoadScene("Level_01");
        PlayerPrefs.SetInt("CarIndexValue", currentCarIndex);
    }

}
