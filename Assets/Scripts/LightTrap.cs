using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LightTrap : MonoBehaviour
{
    public float interval;

    //SpriteRenderer spriteRenderer;
    private UnityEngine.Rendering.Universal.Light2D light2d;
    private BoxCollider2D boxCollider;

    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        light2d = GetComponentInChildren<UnityEngine.Rendering.Universal.Light2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        //Color c = spriteRenderer.color;

        yield return new WaitForSeconds(interval);

        light2d.intensity = 0f;
        yield return new WaitForSeconds(0.1f);
        light2d.intensity = 1f;
        yield return new WaitForSeconds(0.1f);

        boxCollider.enabled = false;

        for (int i = 20; i >= 0; i--)
        {
            light2d.intensity = i / 20f;
            //c.a = i / 50f;
            //spriteRenderer.color = c;
            yield return new WaitForSeconds(0.001f);
        }

        yield return new WaitForSeconds(interval);

        for (int i = 0; i <= 20; i++)
        {
            light2d.intensity = i / 20f;
            //c.a = i / 50f;
            //spriteRenderer.color = c;
            yield return new WaitForSeconds(0.001f);
        }

        boxCollider.enabled = true;

        StartCoroutine("FadeOut");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();

            player.curHealth -= 20;
        }
    }
}
