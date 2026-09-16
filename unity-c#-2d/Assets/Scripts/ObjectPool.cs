using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject poolObject;
    [SerializeField] int numToCreate = 20;
    List<GameObject> createdObjects = new();
    private int index;

    private void Awake()
    {
        for(int i = 0; i < numToCreate; i++)
        {
            CreateObject();
        }
    }
    GameObject CreateObject()
    {
        GameObject createdObject = Instantiate(poolObject, transform);
        createdObjects.Add(createdObject);
        return createdObject;
    }
}
