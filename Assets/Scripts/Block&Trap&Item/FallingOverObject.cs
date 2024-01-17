using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingOverObject : MonoBehaviour
{
    public float objectRot;
    public float delta;

    private bool isFallOver;

    private void Awake()
    {
        isFallOver = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFallOver && collision.gameObject.tag == "Player")
        {
            isFallOver = true;
            StartCoroutine("FallOver");
        }
    }

    IEnumerator FallOver()
    {
        while (Mathf.Abs(delta) <= Mathf.Abs(objectRot))
        {
            transform.rotation = Quaternion.Euler(0, transform.localEulerAngles.y, -1 * delta);
            delta *= 1.02f;
            yield return new WaitForSeconds(0.001f);
        }
        transform.rotation = Quaternion.Euler(0, transform.localEulerAngles.y, -1 * objectRot);
    }
}
