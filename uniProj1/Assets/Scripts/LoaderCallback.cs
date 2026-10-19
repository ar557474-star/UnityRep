using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;
    private void Update()
    {
        if (isFirstUpdate)
        {
            //Debug.Log("First Update");
            isFirstUpdate = false;
            Loader.LoaderCallback();
        }

    }
}
