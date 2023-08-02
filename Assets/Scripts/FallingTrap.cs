using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 연필, 지우개 제어
 * Trigger Collider에 플레이어 진입 시 떨어짐
 * Non-Trigger Collider에 플레이어 충돌 시 데미지 주고 사라짐
 * Non-Trigger Collider에 바닥, 가시 충돌 시 사라짐
 * 사라진 후 2초 뒤에 재생성
 */

[RequireComponent(typeof(Rigidbody2D))]
public class FallingTrap : MonoBehaviour
{   
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    Vector2 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        spawnPoint = transform.position;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rigid.isKinematic = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.curHealth -= 20;
            
            gameObject.SetActive(false);
            Invoke("Init", 2);
        }

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Trap")
        {
            gameObject.SetActive(false);
            Invoke("Init", 2);
        }
    }

    private void Init()
    {
        rigid.velocity = new Vector2(0, 0);
        transform.position = spawnPoint;
        gameObject.SetActive(true);
        
        StartCoroutine("FadeIn");
        rigid.isKinematic = true;
    }
}
