using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public int level = 1;

    public enum states { idle, patrol, scared };
    public states curState = states.idle;

    public float speed = 4f;
    Rigidbody2D bod;
    GameController cont;
    public Transform target;
    int pathIndex = 0;

    Vector3[] path;

    private void Awake()
    {
        cont = FindObjectOfType<GameController>();
        target = cont.GetTarget();
    }

    private void Update()
    {
        switch(curState)
        {
            case states.idle:
                break;
            case states.patrol:
                break;
            case states.scared:
                break;
        }
    }


    private void Start()
    {
        PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = waypoints;

            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        pathIndex = 0;
        Vector3 currentWaypoint = path[0];
        //transform.LookAt(path.lookPoints[0]);

        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                pathIndex++;
                if (pathIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[pathIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = pathIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == pathIndex)
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
