using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

   private void OnTriggerEnter(Collider other)
    {
        Destroy(other.transform.parent.gameObject);
    }
}
