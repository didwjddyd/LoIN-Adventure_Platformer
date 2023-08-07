using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Vector3 initialPosition;
    private bool isMoving = true; // 이동 여부 판단
    private bool isMovingRight = true; // 방향 판단

    public float moveSpeed = 3f;    // 몬스터의 이동 속도
    public float moveDistance = 4f; // 이동 거리
    public float pauseTime = 1f; // 일정 거리 이동 후 쉬는 시간

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider;

    void Awake()
    {
        initialPosition = transform.position; // 맨 처음 몬스터의 위치

        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        StartCoroutine("Move");
    }

    IEnumerator Move()
    {
        // 몬스터의 처음 위치와 이동 후 위치의 차이
        float distance = Vector3.Distance(transform.position, initialPosition);

        if (isMoving)
        {
            if (distance > moveDistance)
            {
                isMoving = false;
                rigid.velocity = Vector2.zero;
                anim.SetBool("isRunning", false);

                yield return new WaitForSeconds(pauseTime);

                // 멈춘 상태에서 initialPosition 초기화
                initialPosition = transform.position;

                spriteRenderer.flipX = !isMovingRight; // sprite를 좌우로 반전
                isMovingRight = !isMovingRight; // 이동 방향을 반대로 설정
                isMoving = true;
            }
            else
            {
                float direction;

                // 설정한 방향 따라 이동
                if (isMovingRight)
                {
                    direction = moveSpeed;
                }
                else
                {
                    direction = -moveSpeed;
                }

                rigid.velocity = new Vector2(direction, rigid.velocity.y);
                anim.SetBool("isRunning", true);
            }

        }
        else
        {
            yield return null;
        }

        yield return null;
    }
}
