using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    private RectTransform rectTransform;

    public float Speed;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        transform.Translate(Vector2.up * Speed * Time.deltaTime);
        if (rectTransform.localPosition.y > 5000f)
        {
            Speed = 0;
            SceneVariable.clearState = 3;
            SceneManager.LoadScene("Start");
        }
    }
}
