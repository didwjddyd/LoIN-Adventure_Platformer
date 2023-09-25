using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 주황 발판 제어
 * 좌우로 delta만큼 이동
 */

public class MovingBlock : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector3 pos; // 현재위치
    public float delta; // 좌우로 이동가능한 (x)최대값
    float speed = 1f; // 이동속도

    void Start()
    {   
        rigid = GetComponent<Rigidbody2D>();
        pos = transform.position;
    }


    void Update()
    { 
        Vector3 v = pos;
        v.x += delta * Mathf.Sin(Time.time * speed);
        transform.position = v;
    }
}
