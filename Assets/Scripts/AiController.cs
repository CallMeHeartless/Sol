using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour {

    private float MaxSpeed = 10.0f;
    private Vector3 currentPos;
    private Vector3 movement;
    private float Radius = 20.0f;
    private Vector3 targetPos;
    private int sequence = 1;
    private float distance;
    private float CurrentSpeed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentPos = transform.position;

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

        //distance = Mathf.Sqrt(Mathf.Pow(2, (targetPos.x - currentPos.x)) + Mathf.Pow(2, (targetPos.z - currentPos.z)));
        distance = (targetPos - transform.position).magnitude;

        float step = 0;

        if(distance < Radius)
        {
            step = MaxSpeed * (distance / Radius) * Time.deltaTime + 0.01f;
            CurrentSpeed = step;
        }
        else
        {
            if(CurrentSpeed < MaxSpeed)
            {
                CurrentSpeed = CurrentSpeed + 0.1f;
            }

            
            step = CurrentSpeed * Time.deltaTime;
        }

        
  

        transform.position = Vector3.MoveTowards(currentPos, targetPos, step);

        if (currentPos == targetPos)
        {
            sequence++;
            if(sequence > 3)
            {
                sequence = 0;
            }
        }
	}
}
