using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class SceneVariable
{
    public static int clearState = 3;
}

public class SelectSceneHandler : MonoBehaviour
{
    [Header("Image Sprite")]
    public Sprite disableMark;
    public Sprite enableMark;
    public Sprite offStar;
    public Sprite onStar;
    public Sprite stage1Image;
    public Sprite stage2Image;
    public Sprite stage3Image;

    [Header("Bookmark")]
    public GameObject stage1Button;
    public GameObject stage2Button;
    public GameObject stage3Button;

    [Header("Timer")]
    public Text time;

    [Header("Enemy")]
    public GameObject stage1Enemy;
    public GameObject stage2Enemy;
    public GameObject stage3Enemy;

    [Header("Stage Clear Recognition")]
    public GameObject stamp;
    public GameObject locker;

    [Header("Stage Infomation")]
    public Text subText;
    public Text numText;
    public Text infoText;
    public List<GameObject> stars;
    public GameObject stageImage;

    [Header("Transition")]
    public Slider transition;

    [Header("Menu")]
    public GameObject menuUI;

    [Header("Audio")]
    public AudioSource audioSource;

    string selectedStage;

    private void Start()
    {
        OnStage1Button();
    }

    void SetStar(int n)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < n)
                stars[i].GetComponent<Image>().sprite = onStar;
            else
                stars[i].GetComponent<Image>().sprite = offStar;
        }
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void OnStage1Button()
    {
        if (selectedStage != "Stage1")
        {
            selectedStage = "Stage1";

            PlaySound();

            stage1Button.GetComponent<Image>().sprite = enableMark;
            stage2Button.GetComponent<Image>().sprite = disableMark;
            stage3Button.GetComponent<Image>().sprite = disableMark;

            time.text = "7:00";

            stage1Enemy.SetActive(true);
            stage2Enemy.SetActive(false);
            stage3Enemy.SetActive(false);

            if (SceneVariable.clearState > 0)
                stamp.SetActive(true);
            else
                stamp.SetActive(false);

            locker.SetActive(false);

            subText.text = "������ �ι��븦 ����";
            numText.text = "Stage 1";
            infoText.text = "���б��� Ż���ϱ� ���ؼ� 3���� STAGE�� ����ؾ� �Ѵ�. �츮���� �ð��� �˹��ϴ�.\n" +
                "STAGE 1 - ������ �ι��븦 ������ ���� ����, �߰����� ����� �Ǵµ�...";

            SetStar(1);

            stageImage.GetComponent<Image>().sprite = stage1Image;
        }
    }

    public void OnStage2Button()
    {
        if (selectedStage != "Stage2")
        {
            selectedStage = "Stage2";

            PlaySound();

            stage1Button.GetComponent<Image>().sprite = disableMark;
            stage2Button.GetComponent<Image>().sprite = enableMark;
            stage3Button.GetComponent<Image>().sprite = disableMark;

            time.text = "10:00";

            stage1Enemy.SetActive(false);
            stage2Enemy.SetActive(true);
            stage3Enemy.SetActive(false);

            if (SceneVariable.clearState > 1)
                stamp.SetActive(true);
            else
                stamp.SetActive(false);

            if (SceneVariable.clearState > 0)
                locker.SetActive(false);
            else
                locker.SetActive(true);

            subText.text = "�ڿ��븦 ����";
            numText.text = "Stage 2";
            infoText.text = "���� ������ ���� ���� �� ����.\n" +
                "STAGE 2�� STAGE 1�� ���� ���̵��� ���ٰ� �˷��� �ִ�. �ڿ��밡 �̷��� ��������... �����Ⱑ �ſ� �����ϰ� ��Ӵ�.";

            SetStar(3);

            stageImage.GetComponent<Image>().sprite = stage2Image;
        }
    }

    public void OnStage3Button()
    {
        if (selectedStage != "Stage3")
        {
            selectedStage = "Stage3";

            PlaySound();

            stage1Button.GetComponent<Image>().sprite = disableMark;
            stage2Button.GetComponent<Image>().sprite = disableMark;
            stage3Button.GetComponent<Image>().sprite = enableMark;

            time.text = "13:00";

            stage1Enemy.SetActive(false);
            stage2Enemy.SetActive(false);
            stage3Enemy.SetActive(true);

            if (SceneVariable.clearState > 2)
                stamp.SetActive(true);
            else
                stamp.SetActive(false);

            if (SceneVariable.clearState > 1)
                locker.SetActive(false);
            else
                locker.SetActive(true);

            subText.text = "�̴�� ����, �׸��� ����";
            numText.text = "Stage 3";
            infoText.text = "������ STAGE���� �����ߴ�.\n���⼭ ���� �� ����.\n" +
                "�̴�� ������ ���� ���� ��򰡿� ���� ������ �츮�� ��ٸ��� �ִٰ� �ϴµ�...";

            SetStar(5);

            stageImage.GetComponent<Image>().sprite = stage3Image;
        }
    }

    public void StartGame()
    {
        if (locker.activeSelf == false)
            StartCoroutine("StartFadeout");
        else
            StartCoroutine("Lock");
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Start");
    }

    public void MenuButton()
    {
        if (menuUI.activeSelf == false)
            menuUI.SetActive(true);
        else
            menuUI.SetActive(false);
    }

    IEnumerator StartFadeout()
    {
        transition.transform.localScale = new Vector3(1, 2, 1);
        while (transition.value < 1)
        {
            transition.value += 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(selectedStage);
    }

    IEnumerator Lock()
    {
        for(int i = 0; i < 5; i++)
        {
            locker.transform.localPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            yield return new WaitForSeconds(0.05f);
        }
        locker.transform.localPosition = Vector3.zero;
    }
}
