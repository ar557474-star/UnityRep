using UnityEngine;

public class Rotate : MonoBehaviour
{
    

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0.3f, 0);
    }
}
