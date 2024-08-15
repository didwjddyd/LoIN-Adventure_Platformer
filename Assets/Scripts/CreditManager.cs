using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource stageAudio;

    public void PlaySound()
    {
        stageAudio.Play();
    }

    public void ExitGame()
    {
        PlaySound();

        SceneManager.LoadScene("Start");
    }
}
