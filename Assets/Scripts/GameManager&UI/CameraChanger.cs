using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraChanger : MonoBehaviour
{
    int cameraPriorityOffset = 7;
    int currentFloor;
    
    public CinemachineVirtualCamera[] cameras;

    public AudioSource[] BGMAudio;

    public int[] floorIndex;

    private void Start()
    {
        currentFloor = floorIndex[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "CameraConfiner")
        {
            int index = collision.gameObject.layer - cameraPriorityOffset; // 0 ~ 4

            print("Enter Layer: " + collision.gameObject.layer);
            print("Enter: " + index);

            cameras[index].Priority += cameraPriorityOffset;

            if(currentFloor != floorIndex[index])
            {
                BGMAudio[currentFloor - 1].Stop();
                BGMAudio[floorIndex[index] - 1].Play();

                currentFloor = floorIndex[index];
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "CameraConfiner")
        {
            int index = collision.gameObject.layer - cameraPriorityOffset; // 0 ~ 4

            print("Exit Layer: " + collision.gameObject.layer);
            print("Exit: " + index);

            if(cameras[index].Priority == 11) cameras[index].Priority = 10;
            else cameras[index].Priority -= cameraPriorityOffset;
        }
    }
}
