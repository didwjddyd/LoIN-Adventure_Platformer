using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.maxSpeed = 7f;
            player.jumpPower = 15f;

            collision.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            
            gameObject.SetActive(false);
        }
    }
}
