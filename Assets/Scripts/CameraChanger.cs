using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChanger : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras = new CinemachineVirtualCamera[3];

    float priorityOffset = 10f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
