// Taken from https://www.youtube.com/watch?v=2mJyaTNPFFc

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float yOffset = 0.0f;
    public float yAngleMin = 15.0f;
    public float yAngleMax = 25.0f;
    public GameObject player;
    public float followDistance = -5.0f;
    float currentX = 0.0f;
    float currentY = 0.0f;
    public float Sensitivity = 1.0f;
   

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
            currentX += Input.GetAxis("Mouse X") * Sensitivity;
            currentY += Input.GetAxis("Mouse Y");
        }

        currentY = Mathf.Clamp(currentY, yAngleMin, yAngleMax);
        //currentY = Mathf.Clamp(transform.position.y, yAngleMin, yAngleMax);
    }

    void LateUpdate() {
       transform.position = player.transform.position + Quaternion.Euler(currentY, currentX, 0) * new Vector3(0, yOffset, followDistance);
       transform.LookAt(player.transform.position);
    }
}
