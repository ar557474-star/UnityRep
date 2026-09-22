using UnityEngine;

public class CameraMovemnet : MonoBehaviour
{
    private Transform playerCarTransform;
    [SerializeField] float offset = -15;
    void Start()
    {
        //playerCarTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    public void SetTransform(Transform transform)
    {
        playerCarTransform = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(playerCarTransform == null)
        {
            return;
        }

        Vector3 cameraPos = transform.position;
        cameraPos.z = playerCarTransform.position.z + offset;
        transform.position = cameraPos;
    }
}
