using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour {

    public float MaxSpeed = 1.0f;
    private Vector3 currentPos;
    private Vector3 movement;
    public float Radius = 10.0f;
    private float PlayerRadius = 5.0f;
    private Vector3 targetPos;
    private int sequence = 0;
    private float distance;
    private float CurrentSpeed;
    private Vector3 playerPos;
    private float playerDistance;
    public GameObject player;
    public GameObject[] targets;
    public float rotationSpeed = 10.0f;
    private Quaternion lookRotation;
    private Vector3 direction;
    public bool linearMovement;
    public bool randomMovement;
    public bool forward = true;
    public float fChargeMultiplier = 1.0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //set movement
        if ((linearMovement == false) && (randomMovement == false))
        {
            linearMovement = true;
        }

        //check 1 type of movement
        if(linearMovement == true)
        {
            randomMovement = false;
        }

        //change direction
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

        currentPos = transform.position;

        targetPos = targets[sequence].transform.position;

        //Dertimine distance from target
        distance = (targetPos - transform.position).magnitude;
        //Dertimine distance from player
        playerDistance = (player.transform.position - transform.position).magnitude;

        //Rotate towards target
        //Find vector from sol current pos to target
        direction = (targetPos - currentPos).normalized;
        //create rotation
        lookRotation = Quaternion.LookRotation(direction);
        //rotate over time
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);


        float step = 0;

        //Increase speed if player is near
        if(distance < Radius)
        {
            step = MaxSpeed * (distance / Radius) * Time.deltaTime + 0.01f;
            CurrentSpeed = step;
            if(playerDistance < PlayerRadius)
            {
                step = step * 2;
            }
        }
        //else move slowly
        else
        {
            if(CurrentSpeed < MaxSpeed)
            {
                CurrentSpeed = CurrentSpeed + 0.01f;
            }

            
            step = CurrentSpeed * Time.deltaTime;
            if (playerDistance < PlayerRadius)
            {
                step = step * 2;
            }
        }

        
  
        //move towards target
        transform.position = Vector3.MoveTowards(currentPos, targetPos, step);

        //choose target
        //Random movement
        if(randomMovement == true)
        {
            if (currentPos == targetPos)
            {
                int Dir;
                Dir = Random.Range(1, 3);

                if (Dir == 1)
                {
                    sequence--;
                }
                else
                {
                    sequence++;
                }

                if (sequence == targets.Length)
                {
                    sequence = (targets.Length) - 2;
                }

                if (sequence < 0)
                {
                    sequence = 1;
                }
            }
        }

        //linear movement
        if(linearMovement == true)
        {
            if(currentPos == targetPos)
            {
                if (forward == true)
                {
                    sequence++;
                }
                else
                {
                    sequence--;
                }

                if(sequence == targets.Length)
                {
                    sequence = 0; //(targets.Length) - 2;
                    //forward = false;
                }

                if(sequence < 0)
                {
                    sequence = targets.Length - 1;
                    //forward = true;
                }
            }
            
        }

        

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
	}
}
