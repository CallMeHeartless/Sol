using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject[] powerBoxes;
    public Light directionalLight;
    bool gameOver = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // Check for game over
        if (CheckForVictory() && !gameOver) {
            gameOver = true;
            directionalLight.GetComponent<Light>().intensity = 1.1f;
            // Start couroutine to end game
        }


	}

    // Iterates through all powerboxes and checkes if they have all been fixed
    bool CheckForVictory() {
        foreach(GameObject fuseBox in powerBoxes) {
            if (!fuseBox.GetComponent<PowerBoxController>().isFixed) {
                return false;
            }
        }

        return true;
    }
}
