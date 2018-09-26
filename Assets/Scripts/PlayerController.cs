using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [Header("Control Variables")]
    [SerializeField]
    private float fLookSensitivity = 10.0f;
    [SerializeField]
    private float fSpeed = 5.0f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float fCameraRotation = 0.0f;
    private float fCurrentCameraRotation = 0.0f;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Transform cameraFocus;
    [SerializeField]
    private Vector3 cameraOffset;

    [Header("Gameplay Properties")]
    [SerializeField]
    float charge;
    public float maxCharge = 20.0f;
    static bool isAlive = true;

    float maxSpeed = 5.0f;
    public float turnSpeed = 10.0f;
   

    Rigidbody rb;
    public Light[] eyeLights;
    public AudioSource bgMusic;
    public Animator anim;
    public GameObject hoverEffect;
    public GameObject[] repairEffects;
    public Slider chargeSlider;

    // Sound effects
    public AudioSource weldingFX;
    public AudioSource LowPowerFX;
    bool lowPower = false;

 
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        charge = maxCharge;
        anim = GetComponent<Animator>();
	}

    void Update()
    {
        // Break here if the player is dead
        if (!isAlive) {
            return;
        }

        // Determine movement
        SetMovement();


        // Repair animation
        if (Input.GetKey(KeyCode.E)) {
           if (!anim.GetBool("PlayerIsFixing")) {
                weldingFX.Play();
                anim.SetBool("PlayerIsFixing", true);
                //anim.SetBool("PlayerIsIdling", false);
                foreach(GameObject sparks in repairEffects) {
                    sparks.SetActive(true);
                }
            }
            
        }else if (anim.GetBool("PlayerIsFixing")) {
            anim.SetBool("PlayerIsFixing", false);
           // anim.SetBool("PlayerIsIdling", true);
           foreach(GameObject sparks in repairEffects) {
                sparks.SetActive(false);
            }
            weldingFX.Stop();
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        // Remove input on death
        if (!isAlive) {
            return;
        }

        if (velocity != Vector3.zero) {
            rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        }
        if (rotation != Vector3.zero) {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        }
        //if (_camera != null && fCameraRotation != 0.0f) {
        //    fCurrentCameraRotation -= fCameraRotation;
        //    fCurrentCameraRotation = Mathf.Clamp(fCurrentCameraRotation, -45, 45);
        //    _camera.transform.localEulerAngles = new Vector3(fCurrentCameraRotation, 0, 0);
        //}

        if (_camera != null) {
            _camera.transform.position = cameraFocus.position + cameraFocus.rotation * cameraOffset;
            _camera.transform.LookAt(cameraFocus.position);
            if (fCameraRotation != 0.0f) {
                fCurrentCameraRotation -= fCameraRotation;
                fCurrentCameraRotation = Mathf.Clamp(fCurrentCameraRotation, -45, 45);
               // _camera.transform.rotation = transform.rotation;
               // _camera.transform.
               //_camera.transform.LookAt(cameraFocus.position);
               // _camera.transform.localEulerAngles = new Vector3(fCurrentCameraRotation, transform.rotation.y, 0);
               // _camera.transform.rotation = Quaternion.Euler(fCurrentCameraRotation, 0, 0);
            }
        }
    }

    // Determines variables used for player movement and camera rotation
    private void SetMovement() {
        // Set movement
        Vector3 forwardMovement = transform.forward * Input.GetAxisRaw("Vertical");
        Vector3 sideMovement = transform.right * Input.GetAxisRaw("Horizontal");

        velocity = (forwardMovement + sideMovement).normalized * fSpeed;
        if (velocity.sqrMagnitude > 0) {
           //Anim
        } else {
            anim.SetTrigger("Idle");
         // Anim
        }

        // Set rotation
        rotation = new Vector3(0.0f, Input.GetAxisRaw("Mouse X"), 0.0f) * fLookSensitivity;

        // Camera rotation 
        fCameraRotation = Input.GetAxisRaw("Mouse Y") * fLookSensitivity;
    }

    // Damages the player's charge. To be used from Sol with Time.deltaTime;
    public void DrainCharge(float drain) {
        Debug.Log(charge / maxCharge);
        if (charge/maxCharge <= 0.3f && !lowPower)
        {
            lowPower = true;
            LowPowerFX.Play();
        }else if (lowPower)
        {
            lowPower = false;
            LowPowerFX.Stop();
        }
        //Debug.Log("Drain charge called");
        if (!isAlive) {
            return;
        }
        charge -= drain;
        // Update slider
        chargeSlider.value = charge/maxCharge;


        // Dim lights
        foreach(Light light in eyeLights) {
            light.GetComponent<Light>().intensity = charge / maxCharge;
        }
        // Dim audio
        bgMusic.volume = charge / maxCharge;

        // Kill if out of charge
        if(charge <= 0) {
            isAlive = false;
            anim.SetBool("PlayerIsDead", true);
            //anim.SetBool("PlayerIsIdling", false);
            hoverEffect.SetActive(false);
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
        // Update slider
        chargeSlider.value = charge / maxCharge;

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