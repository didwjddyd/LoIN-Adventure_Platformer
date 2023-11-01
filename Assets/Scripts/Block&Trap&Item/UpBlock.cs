using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� ���� ����
 * ���Ϸ� delta��ŭ �̵�
 */

public class UpBlock : MonoBehaviour
{
    Vector3 pos;
    public float delta; // ���Ϸ� �̵������� (y)�ִ밪
    float speed = 0.7f; // �̵��ӵ�

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
