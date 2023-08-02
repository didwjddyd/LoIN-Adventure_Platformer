using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 볼펜 제어
 * Trigger Collider에 플레이어 진입 시 왼쪽으로 delta만큼 움직임
 * Non-Trigger Collider에 플레이어 충돌 시 데미지 주고 사라짐
 * delta만큼 이동했을 경우 사라짐
 * 사라진 후 2초 뒤에 재생성
 */

[RequireComponent(typeof(Rigidbody2D))]
public class SideTrap : MonoBehaviour
{   
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    Vector2 spawnPoint;

    public float delta = 10f;

    // Start is called before the first frame update
    void Start()
    {   
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPoint = transform.position;
    }

    private void Update()
    {
        if (rigid.position.x <= spawnPoint.x - delta)
        {
            gameObject.SetActive(false);
            Invoke("Init", 2);
        }
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
            rigid.AddForce(Vector2.left * 150.0f); // 수정 필요
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
        gameObject.transform.position = spawnPoint;
        gameObject.SetActive(true);
        
        StartCoroutine("FadeIn");
    }
}
