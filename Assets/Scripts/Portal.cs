using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Player player;
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        float x = collision.transform.position.x;
        float y = collision.transform.position.y;
        if (collision.tag.Equals("Player"))
        {   
            player.transform.position = new Vector2(x, 10 + y);
        }
    }
}
