using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    PlayerController player;
    bool canShop = false;
    public GameObject shopObj;
    public bool[] hasBought;
    public Image[] costumeButtons;
    public GameObject[] houses;
    public GameObject[] mapExits;
    Image curCostume;
    public Text shopText;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        curCostume = costumeButtons[0];
    }
    
    public Transform GetTarget()
    {
        Transform trans = houses[Random.Range(0, houses.Length)].transform;

        return trans;
    }

    private void Update()
    {
        if (canShop && Input.GetKeyDown(KeyCode.E))
        {
            shopObj.SetActive(!shopObj.activeSelf);
        }

        if (Application.isEditor)
        {
            if (Input.GetKey(KeyCode.M))
            {
                player.sp += 10;
            }
        }

        curCostume.color = Color.green;
        shopText.gameObject.SetActive(canShop);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) canShop = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) canShop = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) canShop = false;
        shopObj.SetActive(false);
    }

    public void BuyGhost()
    {
        curCostume.color = Color.white;
        curCostume = costumeButtons[0];
        player.Ghost();
    }

    public void BuySkeleton(int cost)
    {
        if (hasBought[1])
        {
            curCostume.color = Color.white;
            curCostume = costumeButtons[1];
            player.Skeleton();
        }
        if (!hasBought[1] && player.sp >= cost)
        {
            costumeButtons[1].GetComponentInChildren<Text>().text = "Skeleton";
            curCostume.color = Color.white;
            curCostume = costumeButtons[1];
            hasBought[1] = true;
            player.sp -= cost;
            player.Skeleton();
        }
    }

    public void BuyClown(int cost)
    {
        if (hasBought[2])
        {
            curCostume.color = Color.white;
            curCostume = costumeButtons[2];
            player.Clown();
        }
        if (!hasBought[2] && player.sp >= cost)
        {
            costumeButtons[2].GetComponentInChildren<Text>().text = "Clown";
            curCostume.color = Color.white;
            curCostume = costumeButtons[2];
            hasBought[2] = true;
            player.sp -= cost;
            player.Clown();
        }
    }

    public void BuyVampire(int cost)
    {
        if (hasBought[3])
        {
            curCostume.color = Color.white;
            curCostume = costumeButtons[3];
            player.Vampire();
        }
        if (!hasBought[3] && player.sp >= cost)
        {
            costumeButtons[3].GetComponentInChildren<Text>().text = "Vampire";
            curCostume.color = Color.white;
            curCostume = costumeButtons[3];
            hasBought[3] = true;
            player.sp -= cost;
            player.Vampire();
        }
    }

    public void BuyWerewolf(int cost)
    {
        if (hasBought[4])
        {
            curCostume.color = Color.white;
            curCostume = costumeButtons[4];
            player.Werewolf();
        }
        if (!hasBought[4] && player.sp >= cost)
        {
            costumeButtons[4].GetComponentInChildren<Text>().text = "Werewolf";
            curCostume.color = Color.white;
            curCostume = costumeButtons[4];
            hasBought[4] = true;
            player.sp -= cost;
            player.Werewolf();
        }
    }
}
