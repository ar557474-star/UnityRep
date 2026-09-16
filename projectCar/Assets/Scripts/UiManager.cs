using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] TextMeshProUGUI scoreText;
    CarController carController;
    Transform carTransform;
    private float speed = 0f;
    private float distance = 0f;
    private float score = 0f;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI totalScoreText;
    [SerializeField] TextMeshProUGUI totalDistanceText;
    [SerializeField] TextMeshProUGUI maximumSpeedText;
    float maximumSpeed;


    void Awake()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        carController = playerObj.GetComponent<CarController>();
        carTransform = carController.GetComponent<Transform>();
    }
    void Start()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        speed = carController.CarSpeed();
        speedText.text = speed.ToString("0.00" + "km/h");

        distance= carTransform.position.z / 1000;
        distanceText.text = distance.ToString("0.00"+"Km");
        ScoreUi();
        MaximumSpeed();
    }
    void ScoreUi()
    {
        score = carTransform.position.z * 3;
        scoreText.text = score.ToString("0");
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        totalScoreText.text = score.ToString("0");
        totalDistanceText.text = distance.ToString("0.00" + "Km");
        speedText.transform.parent.gameObject.SetActive(false);
        distanceText.transform.parent.gameObject.SetActive(false);
        scoreText.transform.parent.gameObject.SetActive(false);
    }
    void MaximumSpeed()
    {
        float currentSpeed = carController.CarSpeed();
        if(currentSpeed > maximumSpeed)
        {
            maximumSpeed = currentSpeed;
        }
        maximumSpeedText.text = maximumSpeed.ToString("0.0" + "km/h");
    }
    public void TryAgain()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
