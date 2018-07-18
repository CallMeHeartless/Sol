using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5.0f;
    public float turnSpeed = 10.0f;

    Rigidbody rb;
    Camera playerCamera;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 direction = (playerCamera.right * Input.GetAxis("Horizontal")) + (playerCamera.forward * Input.GetAxis("Vertical"));
        direction.y = 0.0f;

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            rb.rotation = Quaterntion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
            rb.velocity = transform.forward * speed;
            // Animation
        } else {
            // Idle animation
        }
	}
}
