﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FloorSelect : MonoBehaviour
{
    public int floorCount;
    public List<GameObject> floors = new List<GameObject>();

    public int activeFloor;
    private Vector2 startPos;
    private Vector2 direction;
    public static FloorSelect _instance;

    public void Awake()
    {
        _instance = this;
    }

    public void GetFloorNumber(GameObject g)
    {
        GameObject child = null;
        for (int i = 0; i < floorCount; i++)
        {
            child = g.transform.GetChild(i).gameObject;
            print($"child {child}");
            //string floorNumber = child.name.Split('_')[0];
            floors.Add(child);
        }
        //activeFloor = floors.Capacity;
    }

    public void OnFloor()
    {
        if (activeFloor <= floors.Capacity)
        { 
            floors.ElementAt(activeFloor-1).SetActive(true);
            if (activeFloor > 0)
            activeFloor--;
        }
    }
    public void OffFloor()
    {
        if (activeFloor <= floorCount && activeFloor >= 0)
        {
            floors.ElementAt(activeFloor).SetActive(false);
            activeFloor++;
        }
    }

    public void Update()
    {
        //Update the Text on the screen depending on current TouchPhase, and the current direction vector
        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    startPos = touch.position;
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    direction = touch.position - startPos;
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    if (touch.position.y > startPos.y)
                    {
                        OnFloor();
                        print($"up");
                    }
                    if (touch.position.y < startPos.y)
                    {
                        OffFloor();
                        print($"down");
                    }
                    break;
            }
        }
    }
}
