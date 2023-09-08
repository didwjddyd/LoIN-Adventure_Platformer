using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 가시 트랩 작동
 * 플레이어 체력 감소
 */

public class Trap : MonoBehaviour
{
    public bool doRespawn;
    public bool doRegen;

    public int damage;

    SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    IEnumerator FadeIn()
    {
        for (int i = 0; i <= 10; i++)
        {
            Color c = spriteRenderer.material.color;
            c.a = i / 10f;
            spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (doRespawn)
                player.Dead();
            else
                player.curHealth -= damage;

            if (doRegen)
            {
                gameObject.SetActive(false);
                Invoke("Init", 2);
            }
        }
    }

    private void Init()
    {
        gameObject.SetActive(true);
        StartCoroutine("FadeIn");
    }
}
