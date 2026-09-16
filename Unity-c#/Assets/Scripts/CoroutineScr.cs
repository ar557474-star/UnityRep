using System.Collections;
using UnityEngine;

public class CoroutineScr : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(MyCoroutine());
    }

    private IEnumerator MyCoroutine()
    {
        Debug.Log("Before");
        yield return new WaitForSeconds(5);
        Debug.Log("After");
    }
}
