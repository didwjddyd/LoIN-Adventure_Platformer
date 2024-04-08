using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ElevatorHandler : MonoBehaviour
{
    public GameObject rightGate;
    public GameObject leftGate;
    public GameObject fadeOutPanel;

    AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.isLive = false;      //stop control
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            fadeOutPanel = GameObject.Find("Canvas").transform.Find("FadeOut").gameObject;

            StartCoroutine(Close(player));
        }
    }

    IEnumerator Close(Player player)
    {
        //bgm off
        player.GetComponent<CameraChanger>().BGMAudio[player.GetComponent<CameraChanger>().currentFloor].Stop();

        //player move to elevator
        while (player.transform.position.x < transform.position.x)
        {
            player.transform.position += new Vector3(0.03f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }

        //arrived
        player.anim.SetBool("isWalk", false);
        player.walkAudio.enabled = false;

        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(0.3f);

        //gate close
        if(rightGate != null)
        {
            rightGate.GetComponent<SpriteRenderer>().sortingOrder = 5;
            leftGate.GetComponent<SpriteRenderer>().sortingOrder = 5;

            float curPos = 2.9f;

            for (int i = 0; i < 20; i++)
            {
                curPos -= 1.3f / 20;
                rightGate.transform.localPosition = new Vector3(curPos, 0, 0);
                leftGate.transform.localPosition = new Vector3(-curPos, 0, 0);
                yield return new WaitForSeconds(0.02f);
            }
        }

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        fadeOutPanel.gameObject.SetActive(true);
        Image image = fadeOutPanel.GetComponent<Image>();

        float alpha = 0;

        for (int i = 0; i < 50; i++)
        {
            alpha += 0.02f;
            image.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0.01f);
        }

        while (audioSource.isPlaying)
        {
            yield return new WaitForSeconds(0.1f);
        }

        ChangeScene();
    }

    void ChangeScene()
    {
        if (SceneManager.GetActiveScene().name == "Stage1") SceneVariable.clearState = 1;
        else if (SceneManager.GetActiveScene().name == "Stage2") SceneVariable.clearState = 2;
        else
        {
            SceneVariable.clearState = 3;
            SceneManager.LoadScene("Ending");
            return;
        }

        SceneManager.LoadScene("UI");
    }
}
