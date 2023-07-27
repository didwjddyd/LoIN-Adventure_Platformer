using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 노랑 발판 제어
 * 플레이어가 발판을 밟고 있을 때, 일정 시간이 지나면 사라짐
 * 사라진 후 일정 시간이 지나면 다시 생성됨
 */

[RequireComponent(typeof(BoxCollider2D))]
public class BreakBlock : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("FadeOut");
            Invoke("ActiveFalse", 0.5f);
            Invoke("ActiveTrue", 5f);
        }
    }

    void ActiveFalse()
    {
        gameObject.SetActive(false);
    }

    void ActiveTrue()
    {
        gameObject.SetActive(true);
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeOut()
    {
        for (int i = 10; i >= 0; i++)
        {
            Color c = spriteRenderer.material.color;
            c.a = i / 10f;
            spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeIn()
    {
        for (int i = 0; i <= 10; i++)
        {
            Color c = spriteRenderer.material.color;
            c.a = i / 10f;
            spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
