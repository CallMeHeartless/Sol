using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && GameManager.CheckForVictory()) {
            // Next level 
            Debug.Log("Loading next level...");
            GameManager.LoadNextLevel();
        }
    }
}
