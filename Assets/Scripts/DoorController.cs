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
        closed = door.transform;
        target = closed;
    }

    // Open door
    public void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") || other.name == "Sol") {
            if(!isLocked && !isMoving) {
                target = open ;
                StartCoroutine(MoveDoor());
            }
        }
    }

    // Close door
    public void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player") || other.name == "Sol") {
            if (!isLocked && !isMoving) {
                target = closed;
                StartCoroutine(MoveDoor());
            }
        }
    }

    IEnumerator MoveDoor() {
        isMoving = true;
        while(door.transform.localPosition != target.position) {
            door.transform.localPosition = Vector3.MoveTowards(door.transform.localPosition, target.position, Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(2);
        isMoving = false;
        yield return null;
    }

    public void Unlock() {
        isLocked = false;
    }
}
