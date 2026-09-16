using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnumScr : MonoBehaviour
{
    private enum en
    {
        Stage1,Stage2,Stage3
    };

    //private State obj;
    private void Start()
    {
        //DictionaryScr.func();
        en enObj = en.Stage2;
        Debug.Log(enObj);
        //en Obj;
        foreach(en i in Enum.GetValues(typeof(en)))
        {
            Debug.Log(i);
        }
        
    }
}
