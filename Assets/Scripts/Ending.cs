using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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
        if (rectTransform.position.y >= 2800f)
        {
            Speed = 0;
        }
    }
}
