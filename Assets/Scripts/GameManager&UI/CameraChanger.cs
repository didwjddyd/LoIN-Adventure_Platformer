using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    int cameraPriorityOffset = 7;

    public CinemachineVirtualCamera[] cameras;

    public AudioSource BGMAudio;
    public AudioClip[] BGM;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "CameraConfiner")
        {
            int index = collision.gameObject.layer - cameraPriorityOffset; // 0 ~ 4

            //print("Enter Layer: " + collision.gameObject.layer);
            //print("Enter: " + index);

            cameras[index].Priority += cameraPriorityOffset;

            BGMAudio.Stop();
            BGMAudio.clip = BGM[index];
            BGMAudio.Play();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "CameraConfiner")
        {
            int index = collision.gameObject.layer - cameraPriorityOffset; // 0 ~ 4

            //print("Exit Layer: " + collision.gameObject.layer);
            //print("Exit: " + index);

            if(cameras[index].Priority == 11) cameras[index].Priority = 10;
            else cameras[index].Priority -= cameraPriorityOffset;
        }
    }
}
