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
    private Transform target;

    void Start() {
        target = closed;
    }

    // Open door
    public void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") || other.name == "Sol") {
            if(!isLocked && !isMoving) {

            }
        }
    }

    // Close door
    public void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player") || other.name == "Sol") {
            if (!isLocked && !isMoving) {

            }
        }
    }

    IEnumerator MoveDoor() {
        isMoving = true;
        while(door.transform.position != target.position) {
            door.transform.position = Vector3.MoveTowards(door.transform.position, target.position, Time.deltaTime);
        }
        yield return new WaitForSeconds(2);
        isMoving = false;
        yield return null;
    }

    public void Unlock() {
        isLocked = false;
    }
}
