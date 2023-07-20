using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpBlock : MonoBehaviour
{
    Vector3 pos;
    public float delta; // 좌(우)로 이동가능한 (x)최대값
    float speed = 0.7f; // 이동속도

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
