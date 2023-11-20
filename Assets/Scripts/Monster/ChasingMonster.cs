using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingMonster : MonoBehaviour
{
    private float initialPosition;
    private bool isCoroutineRunning = false;

    public GameObject targetPlayer = null;
    public bool isTracing = false;
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
        initialPosition = transform.position.x; // 맨 처음 몬스터의 위치

        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (isTracing && !isCoroutineRunning)
        {
            anim.SetBool("isRunning", true);

            StartCoroutine("Trace");
            isCoroutineRunning = true;
        }
    }

    IEnumerator Trace()
    {
        if (isTracing)
        {
            Vector3 playerPos = targetPlayer.transform.position;

            if (transform.position.x < playerPos.x) // 오른쪽
            {
                playerPos.x = Mathf.Abs(playerPos.x);
                rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
                anim.SetBool("isRunning", true);
            }
        }

        yield return new WaitForSeconds(0.1f);
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
