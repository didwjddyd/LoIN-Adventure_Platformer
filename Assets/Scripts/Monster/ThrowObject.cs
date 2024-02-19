using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� Ʈ�� �۵�
 * �÷��̾� ü�� ����
 */

[RequireComponent(typeof(Rigidbody2D))]
public class ThrowObject : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();

            player.GetDamage(damage);
        }

        Destroy(gameObject);
    }
}
