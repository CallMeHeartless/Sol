using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour {

    public GameObject player;
    public NavMeshAgent agent;

    private Ray ray;
    private Animator anim;

    public float GeneratorRadius = 10.0f;

    private float PlayerRadius = 5.0f;
    private float playerDistance;

    public float fChargeMultiplier = 1.0f;
    public GameObject playerArrow;
    private Quaternion arrowRotation;
    private Vector3 arrowDirection;

    public GameObject solArrow;
    private Quaternion solArrowRotation;
    private Vector3 solArrowDirection;

    public GameObject Generator;

    public GameObject FindPlayer()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = 100000.0f;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public void GoToGenerator()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Generator");
        GameObject closest = null;
        float distance = 100000.0f;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        if(closest != null) {
            float GenDistance = (player.transform.position - closest.transform.position).magnitude;

            if ((GenDistance < GeneratorRadius) && !closest.GetComponent<GeneratorPuzzleController>().IsSolved()) {

                Generator = closest;

                Vector3 vA = closest.transform.position;
                vA.y = vA.y + 4.0f;

                ray.origin = vA;
                ray.direction = Vector3.down;

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) {
                    agent.SetDestination(hit.point);
                }
            }

            if((GenDistance < GeneratorRadius) && closest.GetComponent<GeneratorPuzzleController>().IsSolved())
            {
                Generator = null;
            }
        }
    }

    public GameObject FindPlayerArrow()
    {
        GameObject gos;
        gos = GameObject.FindGameObjectWithTag("PlayerArrow");
        
        return gos;
    }

    public GameObject FindSolArrow()
    {
        GameObject gos;
        gos = GameObject.FindGameObjectWithTag("SolArrow");
        
        return gos;
    }

    public void RotateArrows()
    {
        //Rotate Arrow towards Sol
        if (playerArrow != null)
        {
            arrowDirection = (transform.position - playerArrow.transform.position).normalized;
            arrowDirection.y = 0;
            if (arrowDirection != Vector3.zero)
            {
                //create arrow rotation
                arrowRotation = Quaternion.LookRotation(arrowDirection);
                //Rotate
                playerArrow.transform.rotation = Quaternion.Slerp(playerArrow.transform.rotation, arrowRotation, 10);
            }


            if (playerDistance < PlayerRadius)
            {
                playerArrow.SetActive(false);
                solArrow.SetActive(false);
            }
            else
            {
                playerArrow.SetActive(true);
                solArrow.SetActive(true);
            }
        }


        //Rotate towards player
        solArrowDirection = (player.transform.position - solArrow.transform.position).normalized;
        solArrowDirection.y = 0;
        //create new rotation
        if (solArrowDirection != Vector3.zero)
        {
            solArrowRotation = Quaternion.LookRotation(solArrowDirection);
            //Rotate
            solArrow.transform.rotation = Quaternion.Slerp(solArrow.transform.rotation, solArrowRotation, 10);
        }
    }

    public void GoToPlayer()
    {
        Vector3 vA = player.transform.position;
        vA.y = vA.y + 4.0f;

        ray.origin = vA;
        ray.direction = Vector3.down;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            agent.SetDestination(hit.point);
        }
    }

    // Use this for initialization
    void Start () {
        player = FindPlayer();
        playerArrow = FindPlayerArrow();
        solArrow = FindSolArrow();
    }
	
	// Update is called once per frame
	void Update () {
        
        //Dertimine distance from player
        playerDistance = (player.transform.position - transform.position).magnitude;



        RotateArrows();

        GoToPlayer();

        GoToGenerator();
       
	}
}
