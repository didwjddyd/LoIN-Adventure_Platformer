using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private float initialPosition;
    private float maxLeft;
    private float maxRight;
    private int randomMovement;

    public float moveSpeed = 3.5f; // 몬스터의 이동 속도
    public float moveDistance = 6f; // 이동 거리
    public float pauseTime = 1f; // 일정 거리 이동 후 쉬는 시간

    Rigidbody2D rigid;
    //Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;


    void Awake()
    {
        initialPosition = transform.position.x; // 맨 처음 몬스터의 위치
        maxLeft = initialPosition - moveDistance; // 왼쪽 최대 이동
        maxRight = initialPosition + moveDistance; // 오른쪽 최대 이동

        rigid = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
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
        if (randomMovement == 0) // 왼쪽 이동
        {
            spriteRenderer.flipX = true; // sprite를 좌우로 반전
            rigid.velocity = new Vector2(-moveSpeed, rigid.velocity.y);
            //anim.SetBool("isRunning", true);
        }
        else if (randomMovement == 1) // 정지
        {
            rigid.velocity = Vector2.zero;
            //anim.SetBool("isRunning", false);
        }
        else if (randomMovement == 2) // 오른쪽 이동
        {
            spriteRenderer.flipX = false;
            rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
            //anim.SetBool("isRunning", true);
        }

    }
}
