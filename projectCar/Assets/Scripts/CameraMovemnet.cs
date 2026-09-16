using UnityEngine;

public class CameraMovemnet : MonoBehaviour
{
    private Transform playerCarTransform;
    [SerializeField] float offset = -15;
    void Start()
    {
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 cameraPos = transform.position;
        cameraPos.z = playerCarTransform.position.z + offset;
        transform.position = cameraPos;
    }
}
