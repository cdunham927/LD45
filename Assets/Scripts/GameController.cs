using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //PlayerController player;
    public GameObject shopObj;
    public bool[] hasBought;
    //public Text[] buyText;

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

        if (Application.isEditor)
        {
            if (Input.GetKey(KeyCode.M))
            {
                //player.sp += 10;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    public void BuyGhost()
    {
        //player.Ghost();
    }

    public void BuySkeleton(int cost)
    {
        if (hasBought[1])
        {
            //player.Skeleton();
        }
        //if (!hasBought[1] && player.sp >= cost)
        {
            //player.sp -= cost;
            //player.Skeleton();
        }
    }

    public void BuyClown(int cost)
    {
        if (hasBought[2])
        {
            //player.Clown();
        }
        //if (!hasBought[2] && player.sp >= cost)
        {
            //player.sp -= cost;
            //player.Clown();
        }
    }

    public void BuyVampire(int cost)
    {
        if (hasBought[3])
        {
            //player.Vampire();
        }
        //if (!hasBought[3] && player.sp >= cost)
        {
            //player.sp -= cost;
            //player.Vampire();
        }
    }

    public void BuyWerewolf(int cost)
    {
        if (hasBought[4])
        {
            //player.Werewolf();
        }
        //if (!hasBought[4] && player.sp >= cost)
        {
            //player.sp -= cost;
            //player.Werewolf();
        }
    }
}
