using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartElevator : MonoBehaviour
{
    public GameObject rightGate;
    public GameObject leftGate;
    Player player;
    AudioSource audioSource;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();

        GameManager.instance.pauseActive = true;
        player.isLive = false;

        StartCoroutine(Opening());
    }

    IEnumerator Opening()
    {
        audioSource.Play();

        rightGate.GetComponent<SpriteRenderer>().sortingOrder = 5;
        leftGate.GetComponent<SpriteRenderer>().sortingOrder = 5;

        yield return new WaitForSeconds(0.5f);

        float curPos = 1.6f;

        for (int i = 0; i < 120; i++)
        {
            curPos += 1.3f / 120;
            rightGate.transform.localPosition = new Vector3(curPos, 0, 0);
            leftGate.transform.localPosition = new Vector3(-curPos, 0, 0);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(1f);

        rightGate.GetComponent<SpriteRenderer>().sortingOrder = 0;
        leftGate.GetComponent<SpriteRenderer>().sortingOrder = 0;

        for (int i = 0; i < 50; i++)
        {
            curPos -= 1.3f / 50;
            rightGate.transform.localPosition = new Vector3(curPos, 0, 0);
            leftGate.transform.localPosition = new Vector3(-curPos, 0, 0);
            yield return new WaitForSeconds(0.02f);
        }

        GameManager.instance.pauseActive = false;
        player.isLive = true;
    }
}
