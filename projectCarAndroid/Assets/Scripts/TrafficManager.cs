using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TrafficManager : MonoBehaviour
{
    [SerializeField] Transform[] lanes;
    [SerializeField] GameObject[] trafficVehicles;
    private CarController carController;
    private float dynamicTimer;
    void Awake()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        //carController = playerObj.GetComponent<CarController>();
    }
    void Start()
    {
        StartCoroutine(TrafficSpawner());
    }
    public void SetCarControler(CarController controller)
    {
        carController = controller;
    }

    IEnumerator TrafficSpawner()
    {
        //yield return new WaitForSeconds(2f);
        while (true)
        {
            
            if (carController.CarSpeed() > 5f)
            {
                dynamicTimer = Random.Range(50f, 100f) / carController.CarSpeed();
                SpwanTrafficVehicle();
            }
            yield return new WaitForSeconds(dynamicTimer);
        }
    }
    void SpwanTrafficVehicle()
    {
        int randomLaneIndex = Random.Range(0, lanes.Length);
        int randomTrafficVechileIndex = Random.Range(0, trafficVehicles.Length);
        Instantiate(trafficVehicles[randomTrafficVechileIndex], lanes[randomLaneIndex].position, Quaternion.identity);
    }
}
