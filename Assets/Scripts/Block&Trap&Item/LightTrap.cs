using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LightTrap : MonoBehaviour
{
    public float interval;
    public bool isFlash;
    public bool isBackground;

    private UnityEngine.Rendering.Universal.Light2D light2d;
    private BoxCollider2D boxCollider;

    void Start()
    {
        light2d = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        if(!isFlash && !isBackground)
            RandomCall();
        else if(isBackground)
            StartCoroutine("Blink");
    }

    void RandomCall()
    {
        int pattern = Random.Range(1, 4);

        if (pattern == 1)
            StartCoroutine("Pattern1");
        else if (pattern == 2)
            StartCoroutine("Pattern2");
        else
            StartCoroutine("Pattern3");
    }

    IEnumerator Pattern1()
    {
        yield return new WaitForSeconds(interval);

        light2d.intensity = 0f;
        yield return new WaitForSeconds(0.1f);
        light2d.intensity = 1f;
        yield return new WaitForSeconds(0.1f);

        for (int i = 10; i >= 0; i--)
        {
            light2d.intensity = i / 10f;
            yield return new WaitForSeconds(0.001f);
        }

        boxCollider.enabled = false;
        yield return new WaitForSeconds(interval);
        boxCollider.enabled = true;

        for (int i = 0; i <= 10; i++)
        {
            light2d.intensity = i / 10f;
            yield return new WaitForSeconds(0.001f);
        }

        RandomCall();
    }

    IEnumerator Pattern2()
    {
        yield return new WaitForSeconds(interval);

        for (int i = 10; i >= 0; i--)
        {
            light2d.intensity = i / 10f;
            yield return new WaitForSeconds(0.001f);
        }

        boxCollider.enabled = false;
        yield return new WaitForSeconds(interval);
        boxCollider.enabled = true;

        for (int i = 0; i <= 10; i++)
        {
            light2d.intensity = i / 10f;
            yield return new WaitForSeconds(0.001f);
        }

        RandomCall();
    }

    IEnumerator Pattern3()
    {
        yield return new WaitForSeconds(interval);

        light2d.intensity = 0f;
        yield return new WaitForSeconds(0.05f);
        light2d.intensity = 1f;
        yield return new WaitForSeconds(0.05f);
        light2d.intensity = 0f;
        yield return new WaitForSeconds(0.05f);
        light2d.intensity = 1f;
        yield return new WaitForSeconds(0.05f);

        for (int i = 10; i >= 0; i--)
        {
            light2d.intensity = i / 10f;
            yield return new WaitForSeconds(0.001f);
        }

        boxCollider.enabled = false;
        yield return new WaitForSeconds(interval);
        boxCollider.enabled = true;

        light2d.intensity = 1f;
        yield return new WaitForSeconds(0.05f);
        light2d.intensity = 0f;
        yield return new WaitForSeconds(0.05f);

        for (int i = 0; i <= 10; i++)
        {
            light2d.intensity = i / 10f;
            yield return new WaitForSeconds(0.001f);
        }

        RandomCall();
    }

    IEnumerator Blink()
    {
        light2d.intensity = 1f;
        yield return new WaitForSeconds(2f);
        light2d.intensity = 0f;
        yield return new WaitForSeconds(0.1f);
        light2d.intensity = 1f;
        yield return new WaitForSeconds(0.1f);
        light2d.intensity = 0f;
        yield return new WaitForSeconds(0.1f);
        light2d.intensity = 1f;

        StartCoroutine("Blink");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();

            //player.curHealth -= 20;

            player.GetDamage(20); // damage 변수가 없음
        }
    }
}
