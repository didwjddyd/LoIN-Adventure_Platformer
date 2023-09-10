using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCutscene : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    public GameObject gameManager;
    GameManager gameManagerCom;

    public GameObject mainUI;

    public Slider transition;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        gameManagerCom = gameManager.GetComponent<GameManager>();

        StartCoroutine("Move");
    }

    //default state coroutine
    IEnumerator Move()
    {
        anim.SetBool("isWalk", true);

        spriteRenderer.flipX = false;
        rigid.velocity = new Vector2(3, 0);
        yield return new WaitForSeconds(1f);

        spriteRenderer.flipX = true;
        rigid.velocity = new Vector2(-3, 0);
        yield return new WaitForSeconds(1f);

        StartCoroutine("Move");
    }

    //StartButton On Click, start cutscene
    public void CutScene()
    {
        StopCoroutine("Move");
        anim.SetBool("isWalk", false);

        rigid.velocity = new Vector2(0, 0);

        StartCoroutine("StartGame");
    }

    IEnumerator StartGame()
    {
        //MainUI fadeOut
        while (mainUI.GetComponent<CanvasGroup>().alpha > 0)
        {
            mainUI.GetComponent<CanvasGroup>().alpha -= 0.02f;
            yield return new WaitForSeconds(0.01f);
        }
        mainUI.SetActive(false);

        yield return new WaitForSeconds(1f);

        anim.SetBool("isWalk", true);
        anim.speed = 2;

        spriteRenderer.flipX = false;
        rigid.velocity = new Vector2(6, 0);

        yield return new WaitForSeconds(1f);

        //Transition control
        while (transition.value < 1)
        {
            transition.value += 0.05f;
            yield return new WaitForSeconds(0.01f);
        }

        gameManagerCom.NextScene("Stage1");
    }
}
