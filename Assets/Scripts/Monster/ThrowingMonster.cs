using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingMonster : MonoBehaviour
{
    private float initialPosition;
    private float maxLeft;
    private float maxRight;
    private int randomMovement;
    private bool hasThrown = false; // 물체를 던진 상태인지 여부
    private float localScaleOffset;

    //----------Animator Parameter Hash----------
    private int isRunningHash;
    private int throwTriggerHash;
    //----------Animator Parameter Hash----------

    public float moveSpeed = 6f; // 몬스터의 이동 속도
    public float moveDistance = 7f; // 이동 반경
    public float pauseTime = 3f; // 일정 거리 이동 후 쉬는 시간
    public int damage = 20;

    public GameObject[] throwObjects; // 던지는 오브젝트 배열
    public float throwForce = 12f; // 던지는 힘

    public AudioClip throwSound;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    AudioSource monsterAudio;

    void Awake()
    {
        initialPosition = transform.position.x; // 맨 처음 몬스터의 위치
        maxLeft = initialPosition - moveDistance; // 왼쪽 최대 이동
        maxRight = initialPosition + moveDistance; // 오른쪽 최대 이동

        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        monsterAudio = GetComponent<AudioSource>();

        isRunningHash = Animator.StringToHash("isRunning");
        throwTriggerHash = Animator.StringToHash("throwTrigger");

        localScaleOffset = transform.localScale.x;

        //print("maxLeft: " + maxLeft);
        //print("maxRight: " + maxRight);
    }

    void Start()
    {
        //StartCoroutine("Move");
        StartCoroutine(nameof(New_Move));
    }


    void Update()
    {
        //MovePattern(randomMovement);
        New_MovePattern();
    }

    #region Old System
    IEnumerator Move()
    {
        // 0: 왼쪽 이동, 1: 정지, 2: 오른쪽 이동
        randomMovement = Random.Range(0, 3);

        if (transform.position.x <= maxLeft) // 왼쪽 끝에 도달한 경우
        {
            rigid.velocity = Vector2.zero;
            anim.SetBool("isRunning", false);

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

        yield return new WaitForSeconds(4f);

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

            if (!hasThrown) // 물체를 던진 상태가 아니라면 물체를 던짐
            {
                anim.SetBool("isRunning", false);

                hasThrown = true; // 물체를 던진 상태가 됨
                anim.SetTrigger("throwTrigger");
                Invoke("Throw", 0.3f);
            }
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

            if (!hasThrown)
            {
                anim.SetBool("isRunning", false);

                hasThrown = true; // 물체를 던진 상태가 됨
                anim.SetTrigger("throwTrigger");
                Invoke("Throw", 0.3f);

                //anim.SetBool("isRunning", true);
            }
        }

        transform.localScale = newScale;
    }

    void Throw()
    {
        //----------에러 방지를 위해 임시로 선언----------
        Transform throwPoint = gameObject.transform;
        GameObject objectC = new GameObject();
        GameObject objectF = new GameObject();
        //----------에러 방지를 위해 임시로 선언----------

        // 몬스터 이동 멈추기
        rigid.velocity = Vector2.zero;
        int objNum = Random.Range(0, 2); // 랜덤으로 던질 오브젝트 호출

        if (randomMovement == 0) // 왼쪽으로 이동할 때
        {
            // throwPoint보다 2만큼 왼쪽에서 던지기
            Vector3 throwOffset = new Vector3(-2f, 0f, 0f);
            Vector3 throwPosition = throwPoint.position + throwOffset;
            GameObject cloneObject;

            // 물체 생성
            if (objNum == 0)
            {
                cloneObject
                = Instantiate(objectC, throwPosition, Quaternion.identity);
            }
            else
            {
                cloneObject
                = Instantiate(objectF, throwPosition, Quaternion.identity);
            }
            
            Rigidbody2D objectRigid = cloneObject.GetComponent<Rigidbody2D>();

            objectRigid.velocity = -throwPoint.right * throwForce; // 왼쪽으로 던지기

            // 던지는 애니메이션 끝날 때까지 대기
            float throwAniLength = 1f;

            Invoke("Move", throwAniLength);

            // 일정 시간 후 던진 물체 삭제
            Destroy(cloneObject, 1.2f);
        }
        else if (randomMovement == 2) // 오른쪽으로 이동할 때
        {
            // throwPoint보다 2만큼 오른쪽에서 던지기
            Vector3 throwOffset = new Vector3(2f, 0f, 0f);
            Vector3 throwPosition = throwPoint.position + throwOffset;
            GameObject cloneObject;

            // 물체 생성
            if (objNum == 0)
            {
                cloneObject
                = Instantiate(objectC, throwPosition, Quaternion.identity);
            }
            else
            {
                cloneObject
                = Instantiate(objectF, throwPosition, Quaternion.identity);
            }

            Rigidbody2D objectRigid = cloneObject.GetComponent<Rigidbody2D>();

            objectRigid.velocity = throwPoint.right * throwForce; // 오른쪽으로 던지기

            // 던지는 애니메이션 끝날 때까지 대기
            float throwAniLength = 1f;

            Invoke("Move", throwAniLength);

            // 일정 시간 후 던진 물체 삭제
            Destroy(cloneObject, 1.2f);
        }
    }
    #endregion

    //----------Refactoring----------
    /*
     * 1. separate move & attack pattern
     * 2. move attack into coroutine
     */
    IEnumerator New_Move()
    {
        // 0: 공격, 1: 왼쪽 이동, 2: 정지, 3: 오른쪽 이동
        randomMovement = Random.Range(0, 4);

        if(randomMovement == 0) // 0: 공격
        {
            anim.SetTrigger(throwTriggerHash);
            monsterAudio.PlayOneShot(throwSound, 0.6f);
            Invoke(nameof(New_Throw), 0.3f);
        }

        #region 이동반경 제한 기능. 실제로 필요한 기능인지 잘 모르겠음
        /*if (transform.position.x <= maxLeft) // 왼쪽 끝에 도달한 경우
        {
            
            //rigid.velocity = Vector2.zero;
            //anim.SetBool("isRunning", false);
            

            yield return new WaitForSeconds(pauseTime);

            randomMovement = Random.Range(1, 3);
        }
        else if (transform.position.x >= maxRight) // 오른쪽 끝에 도달한 경우
        {
            
            //rigid.velocity = Vector2.zero;
            //anim.SetBool("isRunning", false);
            

            yield return new WaitForSeconds(pauseTime);

            randomMovement = Random.Range(2, 4);
        }*/
        #endregion

        yield return new WaitForSeconds(pauseTime);

        StartCoroutine(nameof(New_Move));
    }

    void New_MovePattern()
    {
        // default, 2: 정지
        rigid.velocity = Vector2.zero;
        anim.SetBool(isRunningHash, false);

        if (randomMovement == 1) // 1: 왼쪽 이동
        {
            transform.localScale = new Vector3(-localScaleOffset, localScaleOffset, localScaleOffset);

            rigid.velocity = new Vector2(-moveSpeed, rigid.velocity.y);
            anim.SetBool(isRunningHash, true);
        }
        else if (randomMovement == 3) // 3: 오른쪽 이동
        {
            transform.localScale = new Vector3(localScaleOffset, localScaleOffset, localScaleOffset);

            rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
            anim.SetBool(isRunningHash, true);
        }
    }

    void New_Throw()
    {
        int objNum = Random.Range(0, 2); // 랜덤으로 던질 오브젝트 호출
        float throwDirection = transform.localScale.x;

        // set cloneObject's spawn point
        Vector3 throwOffset = new Vector3(2f * throwDirection, 0f, 0f);
        Vector3 throwPoint = transform.position + throwOffset;

        // Instantiate
        GameObject cloneObject = Instantiate(throwObjects[objNum], throwPoint, Quaternion.identity);

        // setting cloneObject's RigidBody2D.velocity
        Rigidbody2D objectRigid = cloneObject.GetComponent<Rigidbody2D>();

        objectRigid.velocity = transform.right * throwDirection * throwForce; // 바라보는 방향으로 던지기

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
