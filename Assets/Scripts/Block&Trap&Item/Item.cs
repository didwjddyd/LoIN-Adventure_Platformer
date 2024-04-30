using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool coin = false;
    public bool heart = false;

    public float heal = 0f;

    private void Start()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.OnInit += Item_OnInit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();

            player.otherAudio.clip = player.itemSound;
            player.otherAudio.Play();

            if(coin)
            {
                player.coin++;
                player.DressState();
            }
            else if(heart)
            {
                player.curHealth += heal;
            }

            gameObject.SetActive(false);
        }
    }

    private void Item_OnInit(object sender, EventArgs eventArgs)
    {
        gameObject.SetActive(true);
    }
}
