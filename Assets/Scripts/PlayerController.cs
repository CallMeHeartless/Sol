using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5.0f;
    public float turnSpeed = 10.0f;

    Camera playerCamera;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    void FixedUpdate() {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
