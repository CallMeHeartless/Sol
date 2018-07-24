﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject[] powerBoxes;
    public Light directionalLight;
    //public PlayerController player;
    bool gameOver = false;

	// Use this for initialization
	void Start () {
        //player = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {

        // Check for game over
        if (CheckForVictory() && !gameOver) {
            gameOver = true;
            directionalLight.GetComponent<Light>().intensity = 1.1f;
            // Start couroutine to end game
        }

        if (!PlayerController.IsAlive() && !gameOver) {
            Debug.Log("DEAD");
            // Game over stuff
            gameOver = true;
            StartCoroutine(ReloadLevel());
            //SceneManager.LoadScene(1);
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

    IEnumerator ReloadLevel() {
        yield return new WaitForSeconds(2.5f);

        gameOver = false;
        SceneManager.LoadScene(1);
        PlayerController.MakeAlive();
        yield return null;
    }
}
