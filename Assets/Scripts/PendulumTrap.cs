using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumTrap : MonoBehaviour
{
    public int damage;
    public float angle;

    private float delta;

    void Update()
    {
        delta += Time.deltaTime * 2f;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(delta) * angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.curHealth -= damage;
        }
    }
}
