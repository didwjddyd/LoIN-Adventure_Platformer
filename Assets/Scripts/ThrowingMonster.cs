using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingMonster : MonoBehaviour
{
    private float initialPosition;
    private float maxLeft;
    private float maxRight;
    private int randomMovement;
    private bool hasThrown = false; // 물채를 던진 상태인지 여부

    public float moveSpeed = 6f; // 몬스터의 이동 속도
    public float moveDistance = 7f; // 이동 거리
    public float pauseTime = 2f; // 일정 거리 이동 후 쉬는 시간

    public GameObject throwObject; // 던지는 물체
    public Transform throwPoint; // 물체 던지는 위치
    public float throwForce = 12f; // 던지는 힘
    public float maxThrowDistance = 8f; // 물체의 최대 이동 거리

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

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
            anim.SetBool("isRunning", true);

            yield return new WaitForSeconds(pauseTime);

            randomMovement = Random.Range(1, 3); // 1: 정지, 2: 오른쪽 이동
        }
        else if (transform.position.x >= maxRight) // 오른쪽 끝에 도달한 경우
        {
            rigid.velocity = Vector2.zero;
            anim.SetBool("isRunning", false);

            yield return new WaitForSeconds(pauseTime);

            randomMovement = Random.Range(0, 2); // 0: 왼쪽 이동, 1: 정지
        }

        hasThrown = false;

        yield return new WaitForSeconds(1.5f);

        StartCoroutine("Move");
    }

    void MovePattern(int randomMovement)
    {
        if (randomMovement == 0) // 왼쪽 이동
        {
            spriteRenderer.flipX = true; // sprite를 좌우로 반전
            rigid.velocity = new Vector2(-moveSpeed, rigid.velocity.y);
            anim.SetBool("isRunning", true);

            if (!hasThrown) // 물체를 던진 상태가 아니라면 물체를 던짐
            {
                Invoke("Throw", 0.5f);
                hasThrown = true; // 물체를 던진 상태가 됨
            }

        }
        else if (randomMovement == 1) // 정지
        {
            rigid.velocity = Vector2.zero;
            anim.SetBool("isRunning", false);
        }
        else if (randomMovement == 2) // 오른쪽 이동
        {
            spriteRenderer.flipX = false;
            rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
            anim.SetBool("isRunning", true);

            if (!hasThrown)
            {
                Invoke("Throw", 0.5f);
                hasThrown = true;
            }

        }

    }

    void Throw()
    {
        if (randomMovement == 0) // 왼쪽으로 이동할 때
        {
            // throwPoint보다 2 앞에서 던지기
            Vector3 throwOffset = new Vector3(-2f, 0f, 0f);
            Vector3 throwPosition = throwPoint.position + throwOffset;

            // 물체 생성
            GameObject cloneObject
                = Instantiate(throwObject, throwPosition, Quaternion.identity);
            Rigidbody2D objectRigid = cloneObject.GetComponent<Rigidbody2D>();

            objectRigid.velocity = -throwPoint.right * throwForce; // 왼쪽으로 던지기

            // 일정 시간 후 던진 물체 삭제
            Destroy(cloneObject, 1.5f);
        }
        else if (randomMovement == 2) // 오른쪽으로 이동할 때
        {
            // throwPoint보다 2 앞에서 던지기
            Vector3 throwOffset = new Vector3(2f, 0f, 0f);
            Vector3 throwPosition = throwPoint.position + throwOffset;

            // 물체 생성
            GameObject cloneObject
                = Instantiate(throwObject, throwPosition, Quaternion.identity);
            Rigidbody2D objectRigid = cloneObject.GetComponent<Rigidbody2D>();

            objectRigid.velocity = throwPoint.right * throwForce; // 오른쪽으로 던지기

            // 일정 시간 후 던진 물체 삭제
            Destroy(cloneObject, 1.5f);
        }

    }

}
