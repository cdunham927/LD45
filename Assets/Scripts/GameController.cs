using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //PlayerController player;
    public GameObject shopObj;

    private void Awake()
    {
        //player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            shopObj.SetActive(!shopObj.activeSelf);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    public void BuyCostume(int cost)
    {
        //if (player.money >= cost)
        {

        }
    }
}
