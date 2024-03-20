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
    public AudioClip hurryUpSound;
    public AudioClip gameOverSound;

    public int[] floorIndex;

    private void Start()
    {
        currentFloor = floorIndex[0];
    }

    public void PlayHurryUpSound()
    {
        BGMAudio[0].clip = hurryUpSound;
        BGMAudio[0].Play();
    }

    public void PlayGameOverSound()
    {
        BGMAudio[0].Stop();
        BGMAudio[0].clip = gameOverSound;
        BGMAudio[0].Play();
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
                BGMAudio[currentFloor].Stop();
                BGMAudio[floorIndex[index]].Play();

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
