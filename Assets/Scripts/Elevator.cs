using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject gameManager;
    GameManager gameManagerCom;

    public string sceneName;
    public bool isAcvtive;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerCom = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isAcvtive)
        {
            gameManagerCom.NextScene(sceneName);
        }
    }
}
