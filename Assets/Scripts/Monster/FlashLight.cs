using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public GameObject flashLight;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer.flipX)
        {
            flashLight.transform.localPosition = new Vector3(-0.65f, 0.05f, 0);
            flashLight.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            flashLight.transform.localPosition = new Vector3(0.65f, 0.05f, 0);
            flashLight.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
