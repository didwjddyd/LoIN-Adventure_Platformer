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
        StartCoroutine("Move");
    }


    void FixedUpdate()
    {
        MovePattern(randomMovement);
    }

    IEnumerator Move()
    {
        // 0: 왼쪽 이동, 1: 정지, 2: 오른쪽 이동
        randomMovement = Random.Range(0, 3);

        if (transform.position.x <= maxLeft) // 왼쪽 끝에 도달한 경우
        {
            rigid.velocity = Vector2.zero;
            //anim.SetBool("isRunning", true);

            yield return new WaitForSeconds(pauseTime);

            randomMovement = Random.Range(1, 3); // 1: 정지, 2: 오른쪽 이동
        }
        else if (transform.position.x >= maxRight) // 오른쪽 끝에 도달한 경우
        {
            rigid.velocity = Vector2.zero;
            //anim.SetBool("isRunning", false);

            yield return new WaitForSeconds(pauseTime);

            randomMovement = Random.Range(0, 2); // 0: 왼쪽 이동, 1: 정지
        }

        yield return new WaitForSeconds(pauseTime);

        StartCoroutine("Move");
    }

    void MovePattern(int randomMovement)
    {
        Vector3 newScale = transform.localScale;

        if (randomMovement == 0) // 왼쪽 이동
        {
            newScale.x = -Mathf.Abs(newScale.x);
            rigid.velocity = new Vector2(-moveSpeed, rigid.velocity.y);
            anim.SetBool("isRunning", true);
        }
        else if (randomMovement == 1) // 정지
        {
            rigid.velocity = Vector2.zero;
            anim.SetBool("isRunning", false);
        }
        else if (randomMovement == 2) // 오른쪽 이동
        {
            newScale.x = Mathf.Abs(newScale.x);
            rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
            anim.SetBool("isRunning", true);
        }

        transform.localScale = newScale;
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
                transform.localScale = new Vector3(-1, 1, 1);

                playerPos.x = -Mathf.Abs(playerPos.x);
                rigid.velocity = new Vector2(-moveSpeed, rigid.velocity.y);
                anim.SetBool("isRunning", true);
            }
            else if (playerPos.x > transform.position.x) // 오른쪽
            {
                moveVelocity = Vector3.right;
                transform.localScale = new Vector3(1, 1, 1);

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

            StopCoroutine("Move"); // 기존의 이동 코루틴 정지

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

            StartCoroutine("Move"); // 기존의 이동 코루틴 실행
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
