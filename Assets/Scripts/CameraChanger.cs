using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField]
    int cameraPriorityOffset = 5;

    public CinemachineVirtualCamera[] cameras = new CinemachineVirtualCamera[2];

    float priorityOffset = 10f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 7: // VCamLayer1
                cameras[0].Priority += cameraPriorityOffset;
                break;
            case 9: // VCamLayer3
                cameras[1].Priority += cameraPriorityOffset;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case 7: // VCamLayer1
                cameras[0].Priority -= cameraPriorityOffset;
                break;
            case 9: // VCamLayer3
                cameras[1].Priority -= cameraPriorityOffset;
                break;
        }
    }
}
