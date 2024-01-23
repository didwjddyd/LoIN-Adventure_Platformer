using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrap : MonoBehaviour
{
    public bool doRespawn;
    public bool doRegen;
    public int damage;

    SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    IEnumerator FadeIn()
    {
        for (int i = 0; i <= 10; i++)
        {
            Color c = spriteRenderer.material.color;
            c.a = i / 10f;
            spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);

            Player player = collider.gameObject.GetComponent<Player>();

            if (doRespawn)
                player.Dead();
            else
                player.GetDamage(damage);


            if (doRegen)
                Invoke("Init", 2);
        }
    }

    private void Init()
    {
        gameObject.SetActive(true);
        StartCoroutine("FadeIn");
    }
}
