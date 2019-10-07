using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public enum states { idle, scared };
    public states curState = states.idle;

    GameController cont;
    PlayerController player;

    //Pathfinding
    public Vector3 target;
    public float speed = 2;
    Vector3[] path;
    public bool drawPath;
    int targetIndex;

    //UI
    public GameObject NPCText;
    public string[] scaredText;
    public string[] notScaredText;
    float clearTextCools;

    //Getting scared
    public float runSpeed;
    [Range(0, 1)]
    public float scareChance = 0.75f;

    //Close target
    [Range(0f, 5f)]
    public float closeToTarget;

    //For States
    float idleCools;
    float scaredCools;
    bool stuck = false;
    float stuckCools = 15f;
    float curSpd;

    private void Awake()
    {
        cont = FindObjectOfType<GameController>();
        player = FindObjectOfType<PlayerController>();
        scareChance = Random.Range(0.1f, 0.5f);
    }

    private void Start()
    {
        curSpd = speed;
        target = cont.GetTarget();
        PathRequestManager.RequestPath(transform.position, target, OnPathFound);
    }

    private void Update()
    {
        if (clearTextCools <= 0)
        {
            ClearText();
        }

        if (clearTextCools > 0) clearTextCools -= Time.deltaTime;

        switch(curState)
        {
            case states.idle:
                idle();
                break;
            case states.scared:
                scared();
                break;
        }
    }

    void idle()
    {
        curSpd = speed;
        float distance = Vector2.Distance(transform.position, target);
        //Debug.Log(distance);
        if (distance < closeToTarget)
        {
            idleCools -= Time.deltaTime;
            stuck = false;
        }
        else
        {
            //Might be stuck
            stuck = true;
        }

        if (idleCools <= 0)
        {
            idleCools = Random.Range(2f, 4f);
            target = cont.GetTarget();
            PathRequestManager.RequestPath(transform.position, target, OnPathFound);
        }

        if (stuck)
        {
            stuckCools -= Time.deltaTime;
        }

        if (stuckCools <= 0)
        {
            stuckCools = 20f;
            idleCools = Random.Range(2f, 4f);
            target = cont.GetTarget();
            PathRequestManager.RequestPath(transform.position, target, OnPathFound);
        }
    }

    public void CheckScared(float mod)
    {
        float val = Random.value;
        NPCText.SetActive(true);
        if ((val - mod) < scareChance)
        {
            clearTextCools = 1f;
            player.sp += Random.Range(10, 20);
            curSpd = runSpeed;
            NPCText.GetComponentInChildren<Text>().text = scaredText[Random.Range(0, scaredText.Length)];
            target = cont.GetRunTarget();
            scaredCools = Random.Range(4f, 6f);
            PathRequestManager.RequestPath(transform.position, target, OnPathFound);
            curState = states.scared;
        }
        else
        {
            clearTextCools = 1f;
            NPCText.GetComponentInChildren<Text>().text = notScaredText[Random.Range(0, notScaredText.Length)];
        }
    }

    void ClearText()
    {
        NPCText.GetComponentInChildren<Text>().text = "";
    }

    void scared()
    {
        if (scaredCools > 0) scaredCools -= Time.deltaTime;
        if (scaredCools <= 0)
        {
            NPCText.SetActive(false);
            idleCools = Random.Range(2f, 4f);
            target = cont.GetTarget();
            PathRequestManager.RequestPath(transform.position, target, OnPathFound);
            curState = states.idle;
        }
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
        if (path.Length > 0)
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

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, curSpd * Time.deltaTime);
            yield return null;

            }
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
