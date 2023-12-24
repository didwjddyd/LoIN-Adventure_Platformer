using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    public ChasingMonster chasingMonster;
    public GameObject player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            chasingMonster.gameObject.SetActive(true);
            chasingMonster.targetPlayer = collision.gameObject;
            chasingMonster.isTracing = true;
        }
    }
}
