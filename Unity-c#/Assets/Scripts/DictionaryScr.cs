using System;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryScr : MonoBehaviour
{
    Dictionary<int, string> dic = new Dictionary<int, string>();
    void Start()
    {
        dic.Add(4, "Ahm");
        Debug.Log(dic[4]);
    }
    public static void func()
    {
        Debug.Log("static");
    }
}
