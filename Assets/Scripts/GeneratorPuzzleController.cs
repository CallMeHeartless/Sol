using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorPuzzleController : MonoBehaviour {

    private bool isSolved = false;
    private bool playerHasSolution = false;
    private bool isPlayerSolving = false;

    /*********
     * 0: Right Arrow
     * 1: Up Arrow
     * 2: Left Arrow
     * 3: Down Arrow
     ********/
    int[,] solution = new int[4, 4];
    int solutionIndex = 0;

    // Use this for initialization
    void Start() {
        GenerateSolution();
    }

    // Update is called once per frame
    void Update() {

        // Check if player is currently solving the puzzle
        if (isPlayerSolving) {

        }

    }

    void GenerateSolution() {
        for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 3; ++j) {
                solution[i, j] = Random.Range(0, 3);
            }
        }
    }

    int GetPlayerInput() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            return 0;
        }else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            return 1;
        }else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            return 2;
        }else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            return 3;
        }
        // Default value:
        return 4;
    }

    void ResetProgress() {
        solutionIndex = 0;
    }

}
