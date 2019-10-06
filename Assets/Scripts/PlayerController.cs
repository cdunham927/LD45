using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    //Variables
    public int sp = 0;
    public Text spText;
    public Text spText2;

    //Animator
    Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Physics
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

        //Animations
        anim.SetFloat("moveX", movement.x);
        anim.SetFloat("moveY", movement.y);
        anim.SetInteger("curCostume", (int)curState);

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
        spText.text = "SP: " + sp.ToString();
        spText2.text = "SP: " + sp.ToString();
    }

    

    //Costume Functions
    public void Ghost()
    {
        curState = states.ghost;

    }

    public void Skeleton()
    {
        curState = states.skeleton;
    }

    public void Clown()
    {
        curState = states.clown;
    }

    public void Vampire()
    {
        curState = states.vampire;
    }

    public void Werewolf()
    {
        curState = states.werewolf;
    }
}
