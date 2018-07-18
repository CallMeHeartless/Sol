using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5.0f;
    float maxSpeed = 5.0f;
    public float turnSpeed = 10.0f;

    Rigidbody rb;
    public Camera playerCamera;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 direction = (playerCamera.transform.right * Input.GetAxis("Horizontal")) + (playerCamera.transform.forward * Input.GetAxis("Vertical"));
        direction.y = 0.0f;

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
            //rb.velocity = transform.forward * speed;
            rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
            if(rb.velocity.magnitude > maxSpeed) {
                rb.velocity = maxSpeed * rb.velocity.normalized;
            }
            // Animation
        } else {
            // Idle animation
            rb.velocity = transform.forward * 0.0f;
        }
	}
}
