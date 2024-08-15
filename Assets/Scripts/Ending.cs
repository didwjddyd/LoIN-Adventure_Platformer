using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [Header("Ending Panel")]
    [SerializeField] private GameObject endingPanel;
    [SerializeField] private GameObject endingTxt;

    [Header("Credit")]
    [SerializeField] private RectTransform creditRectTransform;
    [SerializeField] private float creditSpeed;

    [Header("BGM")]
    [SerializeField] private AudioSource bgmSource;

    [Header("Control End Pannel")]
    public bool isEnding = true;

    void Start()
    {
        if(isEnding)
        {
            endingPanel.SetActive(true);
            endingTxt.SetActive(true);

            StartCoroutine(FadeOut());
        }
        else
        {
            endingPanel.SetActive(false);
            endingTxt.SetActive(false);

            bgmSource.Play();

            StartCoroutine(OnCredit());
        }
    }

    IEnumerator FadeOut()
    {
        bgmSource.Play();

        Text txt = endingTxt.GetComponent<Text>();
        Image image = endingPanel.GetComponent<Image>();

        float alpha = 1;

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(OnCredit());

        for (int i = 0; i < 50; i++)
        {
            alpha -= 0.02f;
            txt.color = new Color(255, 255, 255, alpha);
            image.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0.01f);
        }

        endingPanel.SetActive(false);
        endingTxt.SetActive(false);
    }

    IEnumerator OnCredit()
    {
        while(creditRectTransform.localPosition.y < 5000f)
        {
            creditRectTransform.Translate(Vector2.up * creditSpeed * 0.02f);
            yield return new WaitForSeconds(0.01f);
        }

        SceneVariable.clearState = 3;
        SceneManager.LoadScene("Start");
    }
}
