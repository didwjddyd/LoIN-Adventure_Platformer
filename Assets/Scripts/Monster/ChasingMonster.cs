using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingMonster : MonoBehaviour
{
    private float initialPosition;
    private bool isCoroutineRunning = false;

    public GameObject targetPlayer = null;
    public bool isTracing = false;

    [Range(0f, 10f)]
    public float moveSpeed = 2f; // 몬스터의 이동 속도

    public float moveDistance = 6f; // 이동 거리
    public float pauseTime = 1f; // 일정 거리 이동 후 쉬는 시간
    public int damage = 20;
    public Collider2D detection;

    public GameObject[] throwObjects; // 던지는 오브젝트 배열
    public float throwForce = 12f; // 던지는 힘

    private int isRunningHash;
    private int throwTriggerHash;

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

        isRunningHash = Animator.StringToHash("isRunning");
        throwTriggerHash = Animator.StringToHash("throwTrigger");

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (isTracing && !isCoroutineRunning)
        {
            anim.SetBool(isRunningHash, true);
            StartCoroutine("Trace");
            isCoroutineRunning = true;

            InvokeRepeating("New_Throw", 0.3f, 4f);
        }
    }

    IEnumerator Trace()
    {
        if (isTracing)
        {
            Vector3 playerPos = targetPlayer.transform.position;

            if (transform.position.x < playerPos.x) // 오른쪽
            {
                //playerPos.x = Mathf.Abs(playerPos.x);
                rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
                anim.SetBool(isRunningHash, true);
            }
        }

        yield return new WaitForSeconds(0.1f);
    }

    void New_Throw()
    {
        int objNum = Random.Range(0, 2); //랜덤으로 던질 오브젝트 호출

        //오브젝트 생성 위치 설정
        Vector3 throwOffset = new Vector3(4f, 0f, 0f);
        Vector3 throwPoint = transform.position + throwOffset;

        GameObject cloneObject = Instantiate(throwObjects[objNum], throwPoint, Quaternion.identity);

        //플레이어 위치 찾기
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        //오브젝트 Rigidbody2D 가져오기
        Rigidbody2D objectRigid = cloneObject.GetComponent<Rigidbody2D>();

        //플레이어를 향해 오브젝트 발사
        Vector2 throwDirection = (playerPosition - throwPoint).normalized;
        objectRigid.velocity = transform.right * throwDirection * throwForce;

        anim.SetTrigger(throwTriggerHash);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.GetDamage(damage);
        }
    }
}
