using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] Heart;
    public GameObject player;
    Player playerCom;

    // Start is called before the first frame update
    void Start()
    {
        playerCom = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Heart[0].fillAmount = playerCom.maxHealth / 20f;
    }
}
