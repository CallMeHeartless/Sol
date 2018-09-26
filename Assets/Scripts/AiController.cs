using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour {

    public GameObject player;
    public NavMeshAgent agent;
    public float fAttackRadius = 2.0f;

    private bool bIsStunned = false;
    private bool bIsAttacking = false;
    private float fAttackRate = 0.6f;
    private bool bCanAttack = true;

    private Ray ray;
    private Animator anim;
    private float fDistance;

    public float MaxSpeed = 1.0f;
    private Vector3 currentPos;
    public float Radius = 10.0f;
    private float PlayerRadius = 5.0f;
    private Vector3 targetPos;
    private float distance;
    private Vector3 playerPos;
    private float playerDistance;
    private Quaternion lookRotation;
    private Vector3 direction;
    public float fChargeMultiplier = 1.0f;
    public GameObject playerArrow;
    private Quaternion arrowRotation;
    private Vector3 arrowDirection;

    public GameObject solArrow;
    private Quaternion solArrowRotation;
    private Vector3 solArrowDirection;

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

    // Use this for initialization
    void Start () {
        player = FindPlayer();
        playerArrow = FindPlayerArrow();
        solArrow = FindSolArrow();
    }
	
	// Update is called once per frame
	void Update () {

        //set movement
        /*
        if ((linearMovement == false) && (randomMovement == false))
        {
            linearMovement = true;
        }

        //check 1 type of movement
        if(linearMovement == true)
        {
            randomMovement = false;
        }
        */

        //change direction

        /*
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(forward == true)
            {
                forward = false;
            }
            else
            {
                forward = true;
            }
        }
        */

        currentPos = transform.position;


        //Dertimine distance from target
        distance = (targetPos - transform.position).magnitude;
        //Dertimine distance from player
        playerDistance = (player.transform.position - transform.position).magnitude;

       
        //Rotate Arrow towards Sol
        arrowDirection = (transform.position - playerArrow.transform.position).normalized;
        arrowDirection.y = 0;
        //create arrow rotation
        arrowRotation = Quaternion.LookRotation(arrowDirection);
        //Rotate
        playerArrow.transform.rotation = Quaternion.Slerp(playerArrow.transform.rotation, arrowRotation, 10);

        //Rotate towards next point
        solArrowDirection = (player.transform.position - solArrow.transform.position).normalized;
        solArrowDirection.y = 0;
        //create new rotation
        solArrowRotation = Quaternion.LookRotation(solArrowDirection);
        //Rotate
        solArrow.transform.rotation = Quaternion.Slerp(solArrow.transform.rotation, solArrowRotation, 10);

        ray.origin = player.transform.position;
        ray.direction = Vector3.down;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            agent.SetDestination(hit.point);
        }

        /*
        if(forward == true)
        {
            int i = sequence + 1;
            if(i == targets.Length)
            {
                i = 0;
            }
            //Rotate towards next point
            solArrowDirection = (targets[i].transform.position - solArrow.transform.position).normalized;
            solArrowDirection.y = 0;
            //create new rotation
            solArrowRotation = Quaternion.LookRotation(solArrowDirection);
            //Rotate
            solArrow.transform.rotation = Quaternion.Slerp(solArrow.transform.rotation, solArrowRotation, 10);
        }
        
        else
        {
            int i = sequence - 1;
            if(i < 0)
            {
                i = targets.Length - 1;
            }
            //Rotate towards next point
            solArrowDirection = (targets[i].transform.position - solArrow.transform.position).normalized;
            solArrowDirection.y = 0;
            //create new rotation
            solArrowRotation = Quaternion.LookRotation(solArrowDirection);
            //Rotate
            solArrow.transform.rotation = Quaternion.Slerp(solArrow.transform.rotation, solArrowRotation, 10);
        }
        */

        if (playerDistance < PlayerRadius)
        {
            playerArrow.SetActive(false);
        }
        else
        {
            playerArrow.SetActive(true);
        }


        

        
        

        
        /*
        //Recharge/drain player
        if(playerDistance > PlayerRadius)
        {
            player.GetComponent<PlayerController>().DrainCharge(Time.deltaTime);
            //Debug.Log("Draining Sparky");
        }
        else
        {
            player.GetComponent<PlayerController>().GiveCharge(fChargeMultiplier * Time.deltaTime);
        }
        */
	}
}
