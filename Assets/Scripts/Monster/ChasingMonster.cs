using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingMonster : MonoBehaviour
{
    private float initialPosition;
    private float maxLeft;
    private float maxRight;
    private int randomMovement;
    private GameObject targetPlayer;
    private bool isTracing = false;

    public float moveSpeed = 3.5f; // 몬스터의 이동 속도
    public float moveDistance = 6f; // 이동 거리
    public float pauseTime = 1f; // 일정 거리 이동 후 쉬는 시간
    public int damage = 20;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    CircleCollider2D circleCollider;

    void Awake()
    {
        initialPosition = transform.position.x; // 맨 처음 몬스터의 위치
        maxLeft = initialPosition - moveDistance; // 왼쪽 최대 이동
        maxRight = initialPosition + moveDistance; // 오른쪽 최대 이동

        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {

    }

    void FixedUpdate()
    {

    }

    IEnumerator Trace()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (isTracing)
        {
            Vector3 playerPos = targetPlayer.transform.position;

            if (playerPos.x < transform.position.x) // 왼쪽
            {
                moveVelocity = Vector3.left;
                Vector3 newScale = transform.localScale;
                newScale = new Vector3(-1, 1, 1);

                playerPos.x = -Mathf.Abs(playerPos.x);
                rigid.velocity = new Vector2(-moveSpeed, rigid.velocity.y);
                anim.SetBool("isRunning", true);
            }
            else if (playerPos.x > transform.position.x) // 오른쪽
            {
                moveVelocity = Vector3.right;
                Vector3 newScale = transform.localScale;
                newScale = new Vector3(1, 1, 1);

                playerPos.x = Mathf.Abs(playerPos.x);
                rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
                anim.SetBool("isRunning", true);
            }
        }

        yield return null;
    }

    // 추적 시작
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            targetPlayer = collision.gameObject;

            StartCoroutine("Trace");
        }
    }

    // 추적 중
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTracing = true;
            anim.SetBool("isRunning", true);

            StartCoroutine("Trace");
        }
    }

    // 추적 끝
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTracing = false;

            StopCoroutine("Trace");
        }
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
