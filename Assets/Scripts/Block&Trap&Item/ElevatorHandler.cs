using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().name == "Stage1") SceneVariable.clearState = 1;
            else if (SceneManager.GetActiveScene().name == "Stage2") SceneVariable.clearState = 2;
            else SceneVariable.clearState = 3;

            SceneManager.LoadScene("UI");
        }
    }
}
