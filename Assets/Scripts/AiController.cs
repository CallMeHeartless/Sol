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
    public float rotationSpeed = 10.0f;
    private Quaternion lookRotation;
    private Vector3 direction;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentPos = transform.position;

        //Determine target movement
        if (sequence == 0)
        {
            targetPos = GameObject.Find("Target1").transform.position;
        }

        if (sequence == 1)
        {
            targetPos = GameObject.Find("Target2").transform.position;
        }

        if (sequence == 2)
        {
            targetPos = GameObject.Find("Target3").transform.position;
        }

        if (sequence == 3)
        {
            targetPos = GameObject.Find("Target4").transform.position;
        }

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
        if (currentPos == targetPos)
        {
            int Dir;
            Dir = Random.Range(1, 3);

            if(Dir == 1)
            {
                sequence--;
            }
            else
            {
                sequence++;
            }

            if(sequence > 3)
            {
                sequence = 2;
            }

            if(sequence < 0)
            {
                sequence = 1;
            }
        }

        //Recharge/drain player
        if(playerDistance > PlayerRadius)
        {
            player.GetComponent<PlayerController>().DrainCharge(Time.deltaTime);
        }
        else
        {
            player.GetComponent<PlayerController>().GiveCharge(Time.deltaTime);
        }
	}
}
