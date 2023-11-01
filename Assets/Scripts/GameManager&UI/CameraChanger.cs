using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    int cameraPriorityOffset = 7;

    public CinemachineVirtualCamera[] cameras;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "CameraConfiner")
        {
            int index = collision.gameObject.layer - cameraPriorityOffset; // 0 ~ 4

            //print("Enter Layer: " + collision.gameObject.layer);
            //print("Enter: " + index);

            cameras[index].Priority += cameraPriorityOffset;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "CameraConfiner")
        {
            int index = collision.gameObject.layer - cameraPriorityOffset; // 0 ~ 4

            //print("Exit Layer: " + collision.gameObject.layer);
            //print("Exit: " + index);

            cameras[index].Priority -= cameraPriorityOffset;
        }
    }
}
