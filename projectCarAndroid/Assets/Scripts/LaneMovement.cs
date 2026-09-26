using UnityEngine;

public class LaneMovement : MonoBehaviour
{

    private Transform playerCarTransform;
    [SerializeField] float offset = 50;
    void Start()
    {
      //  playerCarTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    public void SetTransform(Transform transform)
    {
        playerCarTransform = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerCarTransform == null)
        {
            return;
        }
        Vector3 lanePos = transform.position;
        lanePos.z = playerCarTransform.position.z + offset;
        transform.position = lanePos;
    }
}
