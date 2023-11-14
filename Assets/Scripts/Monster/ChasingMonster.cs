using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingMonster : MonoBehaviour
{
    private float initialPosition;
    private GameObject targetPlayer;
    private bool isTracing = false;

    public float moveSpeed = 3.5f; // 몬스터의 이동 속도
    public float moveDistance = 6f; // 이동 거리
    public float pauseTime = 1f; // 일정 거리 이동 후 쉬는 시간
    public int damage = 20;
    public Collider2D detection;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    CircleCollider2D circleCollider;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // 추적 시작
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            targetPlayer = collision.gameObject;

            isTracing = true;
            anim.SetBool("isRunning", true);

            StartCoroutine("Trace");
        }
    }

    IEnumerator Trace()
    {
        if (isTracing)
        {
            Vector3 playerPos = targetPlayer.transform.position;

            if (playerPos.x < transform.position.x) // 왼쪽
            {
                playerPos.x = -Mathf.Abs(playerPos.x);
                rigid.velocity = new Vector2(-moveSpeed, rigid.velocity.y);
                anim.SetBool("isRunning", true);
            }
            else if (playerPos.x > transform.position.x) // 오른쪽
            {
                playerPos.x = Mathf.Abs(playerPos.x);
                rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
                anim.SetBool("isRunning", true);
            }
        }

        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.curHealth -= damage;
        }
    }
}
