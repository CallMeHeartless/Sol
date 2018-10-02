using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorPuzzleController : MonoBehaviour {

    public enum PUZZLE_DIRECTIONS {
        EAST,
        NORTH,
        WEST,
        SOUTH
    }

    //public List<List<PUZZLE_DIRECTIONS>> solution;
    int[,] solution = new int[4,4];

	// Use this for initialization
	void Start () {
        GenerateSolution();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //List<PUZZLE_DIRECTIONS> GenerateSingleSet() {
        
    //}

    void GenerateSolution() {
        for(int i = 0; i < 3; ++i) {
            for(int j = 0; j < 3; ++j) {
                solution[i, j] = Random.Range(0, 3);
            }
        }
    }

    int GenerateCellAnswer() {
        return Random.Range(0, 3);
    }
}
