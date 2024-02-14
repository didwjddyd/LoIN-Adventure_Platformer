using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

/*
 * ���� ����
 * Trigger Collider�� �÷��̾� ���� �� �������� delta��ŭ ������
 * Non-Trigger Collider�� �÷��̾� �浹 �� ������ �ְ� �����
 * delta��ŭ �̵����� ��� �����
 * ����� �� 2�� �ڿ� �����
 */

[RequireComponent(typeof(Rigidbody2D), typeof(AudioSource))]
public class SideTrap : MonoBehaviour
{   
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    AudioSource sideAudio;
    BoxCollider2D[] colls;

    Vector2 spawnPoint;

    [Range(0, 600)]
    public int speed = 300;

    public float delta = 10f;
    public float damage = 20f;

    public AudioClip sideSound;

    // Start is called before the first frame update
    void Start()
    {   
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sideAudio = GetComponent<AudioSource>();
        colls = GetComponents<BoxCollider2D>();

        sideAudio.clip = sideSound;

        spawnPoint = transform.position;
    }

    private void Update()
    {
        if (rigid.position.x <= spawnPoint.x - delta)
        {
            gameObject.SetActive(false);
            Invoke("Init", 2);
        }
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
            rigid.AddForce(Vector2.left * speed); // ���� �ʿ�

            sideAudio.Play();
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
        gameObject.transform.position = spawnPoint;
        gameObject.SetActive(true);
        
        StartCoroutine("FadeIn");
    }
}
