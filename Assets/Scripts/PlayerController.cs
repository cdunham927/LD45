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
    public float sp = 0;
    float curSp;
    public Text spText;
    public Text spText2;
    public float lerpSpd;

    //Animator
    Animator anim;

    //Scaring
    float attackCools = 0;
    public float atkRadius = 1;
    public Text playerText;
    public float scareChanceMod = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void SetText()
    {
        switch(curState)
        {
            case states.ghost:
                playerText.text = "BOO!";
                break;
            case states.skeleton:
                playerText.text = "Rattle me bones!";
                break;
            case states.clown:
                playerText.text = "Come and play with me!";
                break;
            case states.vampire:
                playerText.text = "It'll be painless!";
                break;
            case states.werewolf:
                playerText.text = "You can't run from me!";
                break;
        }
    }

    void Scare()
    {
        SetText();

        Collider2D[] NPCs = Physics2D.OverlapCircleAll(transform.position, atkRadius, 1 << LayerMask.NameToLayer("NPC"));

        foreach(Collider2D npcColl in NPCs)
        {
            npcColl.GetComponent<NPCController>().CheckScared(scareChanceMod);
        }

        Invoke("ClearText", 0.5f);
        attackCools = 0.5f;
    }

    void ClearText()
    {
        playerText.text = "";
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
        /*switch(curState)
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
        }*/

        curSp = Mathf.Lerp(curSp, sp, lerpSpd * Time.deltaTime);

        spText.text = "SP: " + Mathf.RoundToInt(curSp);
        spText2.text = "SP: " + Mathf.RoundToInt(curSp);

        if (Input.GetMouseButtonDown(0) && attackCools <= 0)
        {
            Scare();
        }

        if (attackCools > 0) attackCools -= Time.deltaTime;

        if (Application.isEditor)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                scareChanceMod = 0.5f;
            }
        }
    }

    //Costume Functions
    public void Ghost()
    {
        scareChanceMod = 0;
        curState = states.ghost;
    }

    public void Skeleton()
    {
        scareChanceMod = 0.1f;
        curState = states.skeleton;
    }

    public void Clown()
    {
        scareChanceMod = 0.125f;
        curState = states.clown;
    }

    public void Vampire()
    {
        scareChanceMod = 0.2f;
        curState = states.vampire;
    }

    public void Werewolf()
    {
        scareChanceMod = 0.2f;
        curState = states.werewolf;
    }
}
