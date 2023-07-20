using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

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
    SpriteRenderer spriter;
    Animator anim;

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
        jumpPower = 15f;
        maxSpeed = 3f;
        maxHealth = 20f;
        curHealth = maxHealth;
        isLive = true;
        spawnPoint = new Vector2(-1f, -1.4f);
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        isButton = false;
    }

    private void Update()
    {
        if (isLive)
        {
            if (Input.GetButton("Jump") && !isJumping)
            {
                rigid.velocity = Vector2.zero;
                Vector2 jumpVelocity = Vector2.up * Mathf.Sqrt(jumpPower * -Physics.gravity.y);
                rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
                isJumping = true;
            }
            else if (upValue == 1 && !isJumping)
            {
                rigid.velocity = Vector2.zero;
                Vector2 jumpVelocity = Vector2.up * Mathf.Sqrt(jumpPower * -Physics.gravity.y);
                rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
                isJumping = true;
            }

            // PC
            else if (Input.GetButtonUp("Jump") && rigid.velocity.y > 0)
            {
                // rigid.velocity = rigid.velocity * 0.5f;
            }
            // Mobile
            else if (upValue == 0 && rigid.velocity.y > 0)
            {
                rigid.velocity = rigid.velocity * 0.5f;
            }

            // PC
            if (Input.GetButtonUp("Horizontal"))
            {
                // rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            }
            // Mobile
            else if (leftValue == 0 && rightValue == 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x * 0.5f, rigid.velocity.y);
            }

            if (Input.GetButtonDown("Horizontal"))
            {
                spriter.flipX = Input.GetAxisRaw("Horizontal") == -1;
            }

            else if (leftValue == -1 || rightValue == 1)
            {
                if (leftValue == -1)
                {
                    spriter.flipX = true;
                }
                else
                {
                    spriter.flipX = false;
                }
            }

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
                anim.SetBool("isWalking", false);
            }
            else
            {
                anim.SetBool("isWalking", true);
            }
        }
    }
    void FixedUpdate()
    {
        if (isLive)
        {
            // Move By Key Control
            float hor = Input.GetAxis("Horizontal") + rightValue + leftValue;
            rigid.AddForce(Vector2.right * hor, ForceMode2D.Impulse);

            if (rigid.velocity.x > maxSpeed) // Right Speed;
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < maxSpeed * (-1)) // Left Speed;
                rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }

        // Landing Platform (RayCast)
        /*
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance <= 0.82f)
                {
                    isJumping = false;
                }
            }
        }*/

        // Landing Platform (BoxCast)
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            Vector3 boxSize = new Vector3(1, 1, 1);
            RaycastHit2D boxHit = Physics2D.BoxCast(rigid.position, boxSize/2, 0f,
                Vector2.down,  1f, LayerMask.GetMask("Platform"));
            
            if (boxHit.collider != null)
            {
                if (boxHit.distance <= 0.9f)
                {
                    isJumping = false;
                }
            }
        }
    }


    private void Dead()
    {
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
