using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public int level = 1;

    public enum states { idle, patrol, scared };
    public states curState = states.idle;

    public float spd;
    Rigidbody2D bod;

    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }
}
