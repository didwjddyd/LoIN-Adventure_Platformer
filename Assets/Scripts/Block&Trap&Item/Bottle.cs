using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{

    private void Start()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.OnInit += Bottle_OnInit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();

            player.otherAudio.clip = player.itemSound;
            player.otherAudio.Play();

            player.maxSpeed = 7f;
            player.jumpPower = 15f;

            player.bottle = player.coin + 1;

            collision.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 1f);

            gameObject.SetActive(false);
        }
    }

    private void Bottle_OnInit(object sender, EventArgs eventArgs)
    {
        gameObject.SetActive(true);
    }
}
