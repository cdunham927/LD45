using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Physics 
    private Vector2 movement;           
    private Rigidbody2D rb;  
    public float movementSpeed = 1f;   
    
    //States
    public enum states {ghost, skeleton, clown, vampire, werewolf};
    public states curState = states.ghost;

    //Sprites
    private SpriteRenderer rend;
    public Sprite[] costumes;
    
    //Variables
    public int sp = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Physics
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

        //States
        switch(curState)
        {
            case states.ghost:
                break;
            case states.skeleton:
                break;
            case states.clown:
                break;
            case states.vampire:
                break;
            case states.werewolf:
                break;
        }
    }

    //Costume Functions
    public void Ghost()
    {
        curState = states.ghost;
        rend.sprite = costumes[0];
    }

    public void Skeleton()
    {
        curState = states.skeleton;
        rend.sprite = costumes[1];
    }

    public void Clown()
    {
        curState = states.clown;
        rend.sprite = costumes[2];
    }

    public void Vampire()
    {
        curState = states.vampire;
        rend.sprite = costumes[3];
    }

    public void Werewolf()
    {
        curState = states.werewolf;
        rend.sprite = costumes[4];
    }
}
