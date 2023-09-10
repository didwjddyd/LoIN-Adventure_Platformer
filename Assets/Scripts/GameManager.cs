using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �̱������� ����
    public static GameManager instance;

    public float gameTime;
    public float MaxGameTime = 3 * 60f * 60f;
    public bool Dead = false;

    public bool pauseActive = false;
    public bool titleActive = false;

    public GameObject player;
    Player playerCom;

    public GameObject gameoverUI;
    public GameObject pauseUI;

    public Image Heart;

    public GameObject mainCamera;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name != "Start")
            playerCom = player.GetComponent<Player>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name != "Start")
        {
            if (playerCom.isLive == false)
            {
                // OnPlayerDead();
                gameTime = 0;
            }
            else
            {
                if (!pauseActive && !titleActive)
                {
                    gameTime += Time.deltaTime;

                    if (gameTime > MaxGameTime)
                    {
                        gameTime = MaxGameTime;
                    }
                }
            }

            //HealthUI
            Heart.fillAmount = playerCom.curHealth / 120f;
        }
    }

    public void OnPlayerDead()
    {
        Dead = true;
        gameoverUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause()
    {
        pauseActive = true;
        pauseUI.SetActive(true);
        Time.timeScale = 0;
        mainCamera.GetComponent<UnityEngine.Rendering.Volume>().enabled = true;
    }

    public void Resume()
    {
        pauseActive = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        mainCamera.GetComponent<UnityEngine.Rendering.Volume>().enabled = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void NextScene(string stageName)
    {
        SceneManager.LoadScene(stageName);
    }
}
