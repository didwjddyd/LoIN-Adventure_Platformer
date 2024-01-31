using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ��Ȳ ���� ����
 * �¿�� delta��ŭ �̵�
 */

public class MovingBlock : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector3 pos; // ������ġ

    [Range(0f, 6f)]
    public float delta = 2f; // �¿�� �̵������� (x)�ִ밪
    [Range(0f, 4f)]
    public float speed = 1f; // �̵��ӵ�

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
