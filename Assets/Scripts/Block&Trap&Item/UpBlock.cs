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

    [Range(0f, 6f)]
    public float delta = 2f; // ���Ϸ� �̵������� (y)�ִ밪

    [Range(0f, 4f)]
    public float speed = 0.7f; // �̵��ӵ�

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
