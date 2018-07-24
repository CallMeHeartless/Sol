using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5.0f;
    float maxSpeed = 5.0f;
    public float turnSpeed = 10.0f;
    [SerializeField]
    float charge;
    public float maxCharge = 20.0f;
    static bool isAlive = true;

    Rigidbody rb;
    public Camera playerCamera;
    public Light[] eyeLights;
    public AudioSource bgMusic;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        charge = maxCharge;
	}

    void Update() {
        // Test
        //if (Input.GetKey(KeyCode.K)) {
        //    DrainCharge(Time.deltaTime);
        //} else {
        //    GiveCharge(Time.deltaTime);
        //}
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        // Remove input on death
        if (!isAlive) {
            return;
        }
        Vector3 direction = (playerCamera.transform.right * Input.GetAxis("Horizontal")) + (playerCamera.transform.forward * Input.GetAxis("Vertical"));
        direction.y = 0.0f;

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);

            rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
            if(rb.velocity.magnitude > maxSpeed) {
                rb.velocity = maxSpeed * rb.velocity.normalized;
            }
            // Animation
        } else {
            // Idle animation

            // Limit force
            rb.velocity = transform.forward * 0.0f;
        }
	}

    // Damages the player's charge. To be used from Sol with Time.deltaTime;
    public void DrainCharge(float drain) {
        //Debug.Log("Drain charge called");
        if (!isAlive) {
            return;
        }
        charge -= drain;
        //Debug.Log(charge);

        // Dim lights
        foreach(Light light in eyeLights) {
            light.GetComponent<Light>().intensity = charge / maxCharge;
        }
        // Dim audio
        bgMusic.volume = charge / maxCharge;

        // Kill if out of charge
        if(charge <= 0) {
            isAlive = false;
        }
    }

    public void GiveCharge(float givenCharge) {
        if (!isAlive || charge == maxCharge) {
            return;
        }
        charge += givenCharge;
        if(charge > maxCharge) {
            charge = maxCharge;
        }
       
        // Brighten lights
        foreach (Light light in eyeLights) {
            light.GetComponent<Light>().intensity = charge / maxCharge;
        }

        // Raise audio
        bgMusic.volume = charge / maxCharge;
    }

    public static bool IsAlive() {
        return isAlive;
    }

    public static void MakeAlive() {
        isAlive = true;
    }
}