using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{
    public Player player;
    public GameObject breakBlock;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("FadeOut");
            Invoke("ActiveFalse", 0.5f);
            Invoke("ActiveTrue", 5f);
        }
    }

    void ActiveFalse()
    {
        breakBlock.SetActive(false);
    }

    void ActiveTrue()
    {
        breakBlock.SetActive(true);
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeOut()
    {
        for (int i = 10; i >= 0; i++)
        {
            Color c = spriteRenderer.material.color;
            c.a = i / 10f;
            spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeIn()
    {
        for (int i = 0; i <= 10; i++)
        {
            Color c = spriteRenderer.material.color;
            c.a = i / 10f;
            spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

}
