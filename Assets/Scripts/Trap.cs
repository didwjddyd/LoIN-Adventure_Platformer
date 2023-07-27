using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * ���� Ʈ�� �۵�
 * �÷��̾� ü�� ����
 */

[RequireComponent(typeof(BoxCollider2D))]
public class Trap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();

            player.curHealth -= 20;
        }
    }

}
