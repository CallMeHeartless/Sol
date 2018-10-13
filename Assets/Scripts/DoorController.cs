using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    bool isMoving = false;
    public Transform open;
    public Transform closed;
    public bool isLocked;
    [SerializeField]
    private GameObject door;
    private Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        
    }

    // Open door
    public void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") || other.name == "Sol" && !isLocked) {
            // anim.SetTrigger("Open");
        }
    }

    // Close door
    public void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player") || other.name == "Sol" && !isLocked) {
            // anim.SetTrigger("Close");
        }
    }


    public void Unlock() {
        isLocked = false;
    }
}
