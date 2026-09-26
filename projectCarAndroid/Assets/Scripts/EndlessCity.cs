using UnityEngine;

public class EndlessCity : MonoBehaviour
{
    Transform playerCarTransform;
    [SerializeField] Transform otherCityTransform;
    [SerializeField] float halfLength;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // playerCarTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    public void SetTransform(Transform transform)
    {
        playerCarTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCarTransform.position.z > transform.position.z+halfLength)
        {
            transform.position = new Vector3(0, 0, otherCityTransform.position.z + halfLength * 2);
        }
    }
}
