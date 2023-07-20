using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTrap : MonoBehaviour
{   
    public Player player;
    public GameObject sideTrap;
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

    private void Update()
    {
        if (rigid.position.x <= x - 10)
        {
            sideTrap.SetActive(false);
            Invoke("Init", 2);
        }
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
            rigid.AddForce(Vector2.left * 150.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.curHealth -= 20;
            sideTrap.SetActive(false);
            Invoke("Init", 2);
        }

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Trap")
        {
            sideTrap.SetActive(false);
            Invoke("Init", 2);
        }
    }

    private void Init()
    {
        rigid.velocity = new Vector2(0, 0);
        sideTrap.transform.position = new Vector2(x, y);
        sideTrap.SetActive(true);
        StartCoroutine("FadeIn");
    }
}
