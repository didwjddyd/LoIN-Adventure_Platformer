using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    private Vector3 boxSize;
    
    // non-roof sound
    public AudioClip walkSound;
    public AudioClip jumpSoundStart;
    public AudioClip jumpSoundLand;

    // roof sound
    public AudioClip walkSoundRoof;
    public AudioClip jumpSoundStartRoof;
    public AudioClip jumpSoundLandRoof;

    public AudioClip damageSound;

    AudioClip currentWalkSound;
    AudioClip currentJumpSoundStart;
    AudioClip currentJumpSoundLand;

    public AudioSource walkAudio;
    public AudioSource jumpAudio;
    public AudioSource damageAudio;

    public void Awake()
    {
        jumpPower = 12f;
        maxSpeed = 5f;
        maxHealth = 120f;
        curHealth = maxHealth;
        isLive = true;
        rigid = GetComponent<Rigidbody2D>();

        // set default sound: non-roof
        currentWalkSound = walkSound;
        walkAudio.clip = currentWalkSound;
        walkAudio.loop = true;

        currentJumpSoundStart = jumpSoundStart;
        currentJumpSoundLand = jumpSoundLand;
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

                jumpAudio.clip = currentJumpSoundStart;
                jumpAudio.Play();

                Vector2 jumpVelocity = Vector2.up * jumpPower;
                rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);
                isJumping = true;
            }

            // Move
            if (inputVector.x == 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0f, rigid.velocity.y);
            }
            else
            {
                // Sprite Flip by Move Direction
                spriteRenderer.flipX = inputVector.x == -1;

                // set maxspeed
                if (rigid.velocity.x > maxSpeed) // Right Speed
                    rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
                else if (rigid.velocity.x < maxSpeed * -1) // Left Speed
                    rigid.velocity = new Vector2(maxSpeed * -1, rigid.velocity.y);
            }

            if (curHealth <= 0 || rigid.position.y <= -10)
            {
                Dead();
            }
        }
    }

    void FixedUpdate()
    {
        DressState();

        if (isLive)
        {
            //Move By Key Control
            float hor = inputVector.x;
            rigid.AddForce(Vector2.right * hor, ForceMode2D.Impulse);

            //walking animation
            if (inputVector != new Vector2(0, 0))
            {
                anim.SetBool("isWalk", true);

                // paly walking sound if not jumping
                if (!isJumping) walkAudio.enabled = true;
                else walkAudio.enabled = false;
            }
            else
            {
                anim.SetBool("isWalk", false);

                // turn off walking sound
                walkAudio.enabled = false;
            }
        }

        // Landing Platform using BoxCast
        if (rigid.velocity.y < -2)
        {
            // set box size
            if (transform.localScale == new Vector3(1f, 1f, 1f))
                boxSize = new Vector3(0.9f, 0.5f, 1);
            else
                boxSize = new Vector3(0.6f, 0.35f, 1);

            // BoxCast
            RaycastHit2D boxHit = Physics2D.BoxCast(transform.position, boxSize, 0f,
                Vector2.down, boxSize.y + 0.1f, LayerMask.GetMask("Platform"));

            if (boxHit.collider != null)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);

                isJumping = false;
                inputJump = false;

                jumpAudio.clip = currentJumpSoundLand;
                jumpAudio.Play();

                anim.SetBool("isJump", false);
            }
            else
            {
                inputJump = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Vector3.down * (boxSize.y + 0.1f), boxSize);
    }

    void DressState()
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

    public void GetDamage(float damage)
    {
        curHealth -= damage;
        damageAudio.clip = damageSound;
        damageAudio.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // set roof sound
        if(collision.gameObject.layer == 11) // roof camera confiner layer
        {
            currentWalkSound = walkSoundRoof;
            currentJumpSoundStart = jumpSoundStartRoof;
            currentJumpSoundLand = jumpSoundLandRoof;


            //print(currentWalkSound);
            //print(currentJumpSoundStart);
            //print(currentJumpSoundLand);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // set non-roof sound
        if(collision.gameObject.layer == 11) // roof camera confiner layer
        {
            currentWalkSound = walkSound;
            currentJumpSoundStart = jumpSoundStart;
            currentJumpSoundLand = jumpSoundLand;

            //print(currentWalkSound);
            //print(currentJumpSoundStart);
            //print(currentJumpSoundLand);
        }
    }
}
