using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

/*
 * ����, ���찳 ����
 * Trigger Collider�� �÷��̾� ���� �� ������
 * Non-Trigger Collider�� �÷��̾� �浹 �� ������ �ְ� �����
 * Non-Trigger Collider�� �ٴ�, ���� �浹 �� �����
 * ����� �� 2�� �ڿ� �����
 */

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class FallingTrap : MonoBehaviour
{   
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    AudioSource fallingAudio;
    BoxCollider2D[] colls;

    Vector2 spawnPoint;

    [Range(0f, 4f)]
    public float fallingSpeed = 1;

    public float damage = 20f;

    public AudioClip fallingSound;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        fallingAudio = GetComponent<AudioSource>();
        colls = GetComponents<BoxCollider2D>();

        fallingAudio.clip = fallingSound;

        spawnPoint = transform.position;

        rigid.gravityScale = fallingSpeed;
    }

    IEnumerator FadeIn()
    {
        colls[0].enabled = false;
        colls[1].enabled = false;
        for (int i = 0; i <= 10; i++)
        {
            Color c = spriteRenderer.material.color;
            c.a = i / 10f;
            spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        colls[0].enabled = true;
        colls[1].enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rigid.isKinematic = false;
            fallingAudio.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            //player.curHealth -= damage;
            player.GetDamage(damage);

            gameObject.SetActive(false);
            Invoke("Init", 2);
        }

        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Trap")
        {
            gameObject.SetActive(false);
            Invoke("Init", 2);
        }
    }

    private void Init()
    {
        rigid.velocity = new Vector2(0, 0);
        transform.position = spawnPoint;
        gameObject.SetActive(true);
        
        StartCoroutine("FadeIn");
        rigid.isKinematic = true;
    }
}
