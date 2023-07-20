using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour
{   
    public Player player;
    public GameObject fallingTrap;
    Rigidbody2D rigid;
    SpriteRenderer renderer;

    float x, y;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        x = transform.position.x;
        y = transform.position.y;
    }

    IEnumerator FadeIn()
    {
        for (int i = 0; i <= 10; i++)
        {
            Color c = renderer.material.color;
            c.a = i / 10f;
            renderer.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            rigid.isKinematic = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.curHealth -= 20;
            fallingTrap.SetActive(false);
            Invoke("Init", 2);
        }

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Trap")
        {
            fallingTrap.SetActive(false);
            Invoke("Init", 2);
        }
    }

    private void Init()
    {
        rigid.velocity = new Vector2(0, 0);
        fallingTrap.transform.position = new Vector2(x, y);
        fallingTrap.SetActive(true);
        StartCoroutine("FadeIn");
        rigid.isKinematic = true;
    }
}
