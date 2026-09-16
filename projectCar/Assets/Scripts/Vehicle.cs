using UnityEngine;

public class Vehicle : MonoBehaviour
{
    private float speed=8f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, 0f, speed* Time.deltaTime);
        //VehicleInFront();
        
    }
    /*private void VehicleInFront()
    {
       Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log(hit.collider.gameObject.name + " hit");
            // Draw the incoming ray (Red)
            Debug.DrawLine(transform.position, hit.point, Color.red);


        }
    }*/
    private void VehicleInFront()
    {
        // 1. Set origin offset slightly up in local Y direction
        float yOffset = 0.5f;
        Vector3 castOrigin = transform.position + (transform.up * yOffset);

        // 2. Define half of the box size (width, height, length)
        Vector3 boxHalfExtents = new Vector3(0.8f, 0.2f, 0.5f);

        // 3. Set cast direction and distance
        Vector3 direction = transform.forward;
        float maxDistance = 10f;
        float speed2 = speed;
        // 4. Perform BoxCast
        if (Physics.BoxCast(castOrigin, boxHalfExtents, direction, out RaycastHit hit, transform.rotation, maxDistance))
        {
            Debug.Log(hit.collider.gameObject.name + " hit");

            Debug.DrawLine(castOrigin, hit.point, Color.red);
            if (speed > 0)
            {
                speed -= Time.deltaTime;
            }
        }
        else
        {
            Debug.DrawRay(castOrigin, direction * maxDistance, Color.green);
           speed = speed2;
            transform.Translate(0f, 0f, speed * Time.deltaTime);
        }
       
    }


}
