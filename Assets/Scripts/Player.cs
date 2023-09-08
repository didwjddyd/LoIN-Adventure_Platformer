using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Player 이동
 * 애니메이션 제어
 */

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public float maxHealth;
    public float curHealth;
    public Vector2 spawnPoint;
    public bool isLive;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    bool isJumping = false;

    Vector2 inputVector;
    bool inputJump;

    public GameObject[] dressState;
    public int coin;

    public void Awake()
    {
        jumpPower = 12f;
        maxSpeed = 5f;
        maxHealth = 120f;
        curHealth = maxHealth;
        isLive = true;
        spawnPoint = new Vector2(-1f, -1.4f);
        rigid = GetComponent<Rigidbody2D>();
    }

    #region Input System
    private void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    void OnJump()
    {
        inputJump = true;
        anim.SetBool("isJump", true);
    }
    #endregion

    void Update()
    {
        if (isLive)
        {
            // Jump
            if (inputJump && !isJumping)
            {
                inputJump = false;
                rigid.velocity = Vector2.zero;
                Vector2 jumpVelocity = Vector2.up * jumpPower;
                rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
                isJumping = true;
            }

            // Move
            if (inputVector.x == 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0f, rigid.velocity.y);
            }

            // Sprite Flip by Move Direction
            if (inputVector.x != 0)
            {
                spriteRenderer.flipX = inputVector.x == -1;
            }

            if (curHealth <= 0)
            {
                Dead();
            }
            if (rigid.position.y <= -10)
            {
                Dead();
            }

            if (Mathf.Abs(rigid.velocity.x) < 0.3)
            {
                //anim.SetBool("isWalking", false);
            }
            else
            {
                //anim.SetBool("isWalking", true);
            }
        }
    }

    void FixedUpdate()
    {
        //character dress state
        for (int i = 0; i < dressState.Length; i++)
        {
            if (i == coin)
            {
                dressState[i].SetActive(true);
                spriteRenderer = dressState[i].GetComponent<SpriteRenderer>();
                anim = dressState[i].GetComponent<Animator>();
            }
            else
                dressState[i].SetActive(false);
        }

        if (isLive)
        {
            //Move By Key Control
            float hor = inputVector.x;
            rigid.AddForce(Vector2.right * hor, ForceMode2D.Impulse);

            // set maxspeed
            if (rigid.velocity.x > maxSpeed) // Right Speed
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < maxSpeed * -1) // Left Speed
                rigid.velocity = new Vector2(maxSpeed * -1, rigid.velocity.y);

            //walking animation
            if(inputVector != new Vector2(0, 0))
                anim.SetBool("isWalk", true);
            else
                anim.SetBool("isWalk", false);
        }

        // Landing Platform using BoxCast
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            Vector3 boxSize = new Vector3(1.2f, 1, 1); // set box size

            // BoxCast
            RaycastHit2D boxHit = Physics2D.BoxCast(rigid.position, boxSize / 2, 0f,
                Vector2.down, 2, LayerMask.GetMask("Platform"));

            if (boxHit.collider != null)
            {
                if (boxHit.distance < 1f)
                {
                    isJumping = false;
                    inputJump = false;

                    anim.SetBool("isJump", false);
                }
            }
            else
            {
                inputJump = false;
            }
        }
    }

    public void Dead()
    {
        print("Player Dead");

        isLive = false;
        transform.position = spawnPoint;
        Init();
    }

    private void Init()
    {
        curHealth = maxHealth;
        isLive = true;
        isJumping = false;
        coin = 0;
        transform.localScale = new Vector3(1f, 1f, 1f);
        jumpPower = 12f;
        maxSpeed = 5f;
    }
}
