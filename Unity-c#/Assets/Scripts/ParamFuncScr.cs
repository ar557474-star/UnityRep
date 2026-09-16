using UnityEngine;

public class ParamFuncScr : MonoBehaviour
{
    void Start()
    {
        pr("ahm", "hma", "mah");
    }
    private static void pr(params string[] str)
    {
        foreach(string i in str)
        {
            Debug.Log(i);
        }
    }
    
}
