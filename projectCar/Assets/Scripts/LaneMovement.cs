using UnityEngine;

public class LaneMovement : MonoBehaviour
{

    private Transform playerCarTransform;
    [SerializeField] float offset = 50;
    void Start()
    {
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 lanePos = transform.position;
        lanePos.z = playerCarTransform.position.z + offset;
        transform.position = lanePos;
    }
}
