using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour {

    private float Speed = 0.1f;
    private Vector3 currentPos;
    private Vector3 movement;
    private float Radius = 20.0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentPos = transform.position;
        movement.x = 1.0f;
        movement = movement * Speed;
        transform.position = movement + currentPos;
	}
}
