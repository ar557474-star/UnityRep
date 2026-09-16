using UnityEngine;

public class SingletonP : MonoBehaviour
{
    public static SingletonP Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Instance already exists");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
