using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 보라 발판 제어
 * 상하로 delta만큼 이동
 */

public class UpBlock : MonoBehaviour
{
    Vector3 pos;

    [Range(0f, 4f)]
    public float delta = 2f; // 상하로 이동가능한 (y)최대값

    [Range(0f, 1f)]
    public float speed = 0.7f; // 이동속도

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = pos;
        v.y += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
}
