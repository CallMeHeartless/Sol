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
    private Transform origin;

    void Start() {
        closed = door.transform;
        target = closed;
        origin = open;
    }

    void Update() {
        
    }

    // Open door
    public void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") || other.name == "Sol") {
            if(!isLocked && !isMoving) {
                StartCoroutine(OpenDoor());
            }
        }
    }

    // Close door
    public void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player") || other.name == "Sol") {
            if (!isLocked && !isMoving) {
                StartCoroutine(CloseDoor());
            }
        }
    }

    IEnumerator OpenDoor() {
        //isMoving = true;
        Vector3 position = door.transform.position;
        Vector3 targetPosition = position + new Vector3(1, 0, 0);
        while(door.transform.localPosition != targetPosition) {
            Debug.Log("Open");
            door.transform.localPosition = Vector3.MoveTowards(position, targetPosition, Time.deltaTime);
            yield return null;
           
        }
    }

    IEnumerator CloseDoor() {
        //isMoving = true;
        Vector3 position = door.transform.position;
        Vector3 targetPosition = position + new Vector3(-1, 0, 0);
        while (door.transform.position != targetPosition) {
            Debug.Log("Close");
            door.transform.position = Vector3.MoveTowards(position, targetPosition, Time.deltaTime);
            yield return null;

        }
    }

    public void Unlock() {
        isLocked = false;
    }
}
