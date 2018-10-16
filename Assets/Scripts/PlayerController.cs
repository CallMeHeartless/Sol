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
    bool bCanFire = true;
    [SerializeField]
    private float fFireCooldown = 0.75f;

    //float maxSpeed = 5.0f;
    public float turnSpeed = 10.0f;
   
    // Other properties
    Rigidbody rb;
    public Light[] eyeLights;
    public AudioSource bgMusic;
    Animator anim;
    public GameObject hoverEffect;

    //public GameObject[] repairEffects;

    public Slider chargeSlider;
    public Transform gunPosition;
    //private GameObject muzzleFlash;
    ParticleSystem muzzleFlash;

 
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        charge = maxCharge;
        anim = GetComponentInChildren<Animator>();
        gunPosition = transform.Find("Base HumanGun");
        //muzzleFlash = gunPosition.Find("VFX_MuzzleFlash").GetComponent<ParticleSystem>();
        
	}

    void Update()
    {
        // Break here if the player is dead
        if (!isAlive) {
            return;
        }

        // Determine movement
        SetMovement();

        // Debug test fire
        if (Input.GetButtonDown("Fire1")) {
            anim.ResetTrigger("Idle");
            anim.ResetTrigger("Walk");
            anim.SetTrigger("Attack");
            Debug.Log("Fire");
            //anim.ResetTrigger("Attack");
            //FireWeapon(); // This may be extracted and placed on an animation effect || If so, animation trigger here
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

        if (_camera != null) {
            _camera.transform.position = cameraFocus.position + cameraFocus.rotation * cameraOffset;
            _camera.transform.LookAt(cameraFocus.position);
            if (fCameraRotation != 0.0f) {
                fCurrentCameraRotation -= fCameraRotation;
                fCurrentCameraRotation = Mathf.Clamp(fCurrentCameraRotation, -45, 45);

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
            anim.ResetTrigger("Idle");
            anim.SetTrigger("Walk");
        } else {
            anim.ResetTrigger("Walk");
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
        
        //Debug.Log("Drain charge called");
        if (!isAlive) {
            return;
        }
        charge -= drain;
        // Update slider
        chargeSlider.value = charge/maxCharge;

        if(eyeLights != null) {
            // Dim lights
            foreach (Light light in eyeLights) {
                light.GetComponent<Light>().intensity = charge / maxCharge;
            }
            // Dim audio
            if (bgMusic != null) {
                bgMusic.volume = charge / maxCharge;
            }
        }
    


        // Kill if out of charge
        if(charge <= 0) {
            isAlive = false;
            //anim.SetBool("PlayerIsDead", true);
            anim.SetTrigger("Death");
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

    public void FireWeapon() {
        if (bCanFire) {
            bCanFire = false;
            //muzzleFlash.Play();
            // Replace position with reference to Transform on final model
            GameObject projectile = GameObject.Instantiate(Resources.Load("Projectile", typeof(GameObject)), gunPosition.position , transform.rotation) as GameObject;//+ transform.forward + transform.up

            StartCoroutine(WeaponCooldown());
        }

    }

    IEnumerator WeaponCooldown() {
        yield return new WaitForSeconds(fFireCooldown);
        bCanFire = true;
    }
}