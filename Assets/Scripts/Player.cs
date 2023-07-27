using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

/*
 * Player 이동
 * 애니메이션 제어
 */

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public float maxHealth;
    public float curHealth;
    public Vector2 spawnPoint;
    public bool isLive;
    bool isShifting;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    //Animator anim;

    public bool isJumping = false;

    // Mobile Key Var
    int upValue;
    int leftValue;
    int rightValue;
    int downValue;
    bool upDown;
    bool downDown;
    bool leftDown;
    bool rightDown;
    bool upUp;
    bool leftUp;
    bool rightUp;
    bool isButton;

    // Start is called before the first frame update
    public void Awake()
    {
        jumpPower = 12f;
        maxSpeed = 5f;
        maxHealth = 20f;
        curHealth = maxHealth;
        isLive = true;
        spawnPoint = new Vector2(-1f, -1.4f);
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
        isButton = false;
    }

    private void Update()
    {
        if (isLive)
        {
            // Jump
            if (Input.GetButtonDown("Jump") && !isJumping) // PC version
            {
                rigid.velocity = Vector2.zero;
                Vector2 jumpVelocity = Vector2.up * jumpPower;
                rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
                isJumping = true;
            }
            else if (upValue == 1 && !isJumping)
            {
                //rigid.velocity = Vector2.zero;
                //Vector2 jumpVelocity = Vector2.up * jumpPower;
                //rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
                //isJumping = true;
            }

            // ??
            // PC
            else if (Input.GetButtonUp("Jump") && rigid.velocity.y > 0)
            {
                // rigid.velocity = rigid.velocity * 0.5f;
            }
            // Mobile
            else if (upValue == 0 && rigid.velocity.y > 0)
            {
                //rigid.velocity = rigid.velocity * 0.5f;
            }

            // Move
            if (Input.GetButtonUp("Horizontal")) // PC version
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0f, rigid.velocity.y);
            }
            else if (leftValue == 0 && rightValue == 0) // Mobile version
            {
                //rigid.velocity = new Vector2(rigid.velocity.x * 0.5f, rigid.velocity.y);
            }

            // Sprite Flip by Move Direction
            if (Input.GetButton("Horizontal"))
            {
                spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            }
            else if (leftValue == -1 || rightValue == 1)
            {
                if (leftValue == -1)
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
            }

            // ??
            if (isShifting == true)
            {
                curHealth = 1099999999;
                isShifting = false;
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
        if (isLive)
        {
            //Move By Key Control
            float hor = Input.GetAxisRaw("Horizontal") + rightValue + leftValue;
            rigid.AddForce(Vector2.right * hor, ForceMode2D.Impulse);

            // set maxspeed
            if (rigid.velocity.x > maxSpeed) // Right Speed
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < maxSpeed * -1) // Left Speed
                rigid.velocity = new Vector2(maxSpeed * -1, rigid.velocity.y);
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
                }
            }
        }
    }

    private void Dead()
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
    }

    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "UP":
                upValue = 1;
                upDown = true;
                break;
            case "LEFT":
                leftValue = -1;
                leftDown = true;
                break;
            case "RIGHT":
                rightValue = 1;
                rightDown = true;
                break;
            case "DOWN":
                downValue = -1;
                downDown = true;
                isShifting = true;
                break;
        }
    }

    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "UP":
                upValue = 0;
                upUp = true;
                break;
            case "LEFT":
                leftValue = 0;
                leftUp = true;
                break;
            case "RIGHT":
                rightValue = 0;
                rightUp = true;
                break;
            case "DOWN":
                downValue = 0;
                isShifting = true;
                break;
        }
    }

}
