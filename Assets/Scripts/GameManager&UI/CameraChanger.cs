using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public int cameraPriorityOffset = 7;

    public CinemachineVirtualCamera[] cameras;

    void OnTriggerEnter2D(Collider2D collision)
    {
        int index = collision.gameObject.layer - cameraPriorityOffset; // 0 ~ 4

        cameras[index].Priority += cameraPriorityOffset;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        int index = collision.gameObject.layer - cameraPriorityOffset; // 0 ~ 4

        cameras[index].Priority -= cameraPriorityOffset;
    }
}
