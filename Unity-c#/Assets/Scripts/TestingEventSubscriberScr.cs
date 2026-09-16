using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TestingEventSubscriberScr : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TestingEventsScr testingEventsScr = GetComponent<TestingEventsScr>();
        testingEventsScr.OnSpacePressed += TestingEventsScr_OnSpacePressed;
    }

    private void TestingEventsScr_OnSpacePressed(object sender, EventArgs e)
    {
        Debug.Log("Space scr " + e);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
