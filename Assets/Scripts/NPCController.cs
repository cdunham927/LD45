using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public enum states { idle, patrol, scared, run };
    public states curState = states.idle;

    GameController cont;

    //Pathfinding
    public Vector3 target;
    public float speed = 2;
    Vector3[] path;
    public bool drawPath;
    int targetIndex;

    //UI
    public GameObject NPCText;

    //Getting scared
    public bool isScared;
    [Range(0, 1)]
    public float scareChance = 0.75f;

    //Close target
    [Range(0f, 5f)]
    public float closeRadius;

    //For States
    public float idleCools;
    public float patrolCools;
    public float scaredCools;
    public float runCools;

    private void Awake()
    {
        cont = FindObjectOfType<GameController>();
        //target = cont.GetTarget();
        target = GetCloseTarget();
    }

    private void Update()
    {
        switch(curState)
        {
            case states.idle:
                idle();
                break;
            case states.patrol:
                patrol();
                break;
            case states.scared:
                scared();
                break;
            case states.run:
                run();
                break;
        }
    }

    void idle()
    {
        if (Vector3.Distance(transform.position, target) < 0.5f && idleCools <= 0)
        {
            target = GetCloseTarget();
        }
        idleCools -= Time.deltaTime;
    }

    void patrol()
    {

        patrolCools -= Time.deltaTime;
    }

    void scared()
    {

        scaredCools -= Time.deltaTime;
    }

    void run()
    {

        runCools -= Time.deltaTime;
    }

    void Start()
    {
        PathRequestManager.RequestPath(transform.position, target, OnPathFound);
    }


    public Vector3 GetCloseTarget()
    {
        Vector3 trans = (Vector2)transform.position + Random.insideUnitCircle * closeRadius;
        idleCools = 1f;
        return trans;
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        Vector3 currentWaypoint = path[0];

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;

        }
    }

    public void OnDrawGizmos()
    {
        if (drawPath)
        {
            if (path != null)
            {
                for (int i = targetIndex; i < path.Length; i++)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(path[i], Vector3.one);

                    if (i == targetIndex)
                    {
                        Gizmos.DrawLine(transform.position, path[i]);
                    }
                    else
                    {
                        Gizmos.DrawLine(path[i - 1], path[i]);
                    }
                }
            }
        }
    }
}
