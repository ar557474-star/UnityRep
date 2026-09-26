using Unity.VisualScripting;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] carsPrefab;

    [SerializeField] CameraMovemnet cameraMovemnet;
    [SerializeField] UiManager uiManager;
    [SerializeField] EndlessCity[] cityArray;
    [SerializeField] TrafficManager trafficManager;
    [SerializeField] LaneMovement laneMovement;

    void Start()
    {
        SpawnCar();
    }

   
    void SpawnCar()
    {
        int currentCarIndex = PlayerPrefs.GetInt("CarIndexValue", 0);
        Debug.Log("CarIndex " + currentCarIndex);
        GameObject newCar = Instantiate(carsPrefab[currentCarIndex],transform.position,transform.rotation);
        CarController carController = newCar.GetComponent<CarController>();
        cameraMovemnet.SetTransform(carController.transform);
        uiManager.SetCarControler(carController);
        cityArray[0].SetTransform(carController.transform);
        cityArray[1].SetTransform(carController.transform);
        trafficManager.SetCarControler(carController);
        laneMovement.SetTransform(carController.transform);
    }
}
