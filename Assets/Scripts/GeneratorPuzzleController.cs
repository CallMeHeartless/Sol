using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorPuzzleController : MonoBehaviour {

    public string sectorName;

    private bool isSolved = false;
    private bool isPlayerInRange = false;
    private bool isAISolving = true;
    private bool playerHasSolution = false;
    private bool isPlayerSolving = false;
    [SerializeField]
    private float playerRange = 3.0f;
    private GameObject player;
    [SerializeField]
    private float repairCountdown = 20.0f;
    private float repairCount = 0.0f;
    [SerializeField]
    private Slider repairSlider;

    /*********
     * 0: Right Arrow
     * 1: Up Arrow
     * 2: Left Arrow
     * 3: Down Arrow
     ********/
    int[,] solution = new int[4, 4];
    int solutionIndex = 0;
    int setIndex = 0;

    // Use this for initialization
    void Start() {
        GenerateSolution();
        player = GameObject.Find("Sol");
        repairSlider.maxValue = repairCountdown;
        repairSlider.value = 0;
    }

    // Update is called once per frame
    void Update() {
        if (isSolved) {
            return;
        }

        // Check player is in range to solve puzzle
        TrackPlayer();

        // AI solving
        if(isPlayerInRange && isAISolving) {
            repairCount += Time.deltaTime;
            repairSlider.value = repairCount;
            if(repairCount >= repairCountdown) {
                isSolved = true;
            }
        }


    }

    void GenerateSolution() {
        for (int i = 0; i < 4; ++i) {
            for (int j = 0; j < 4; ++j) {
                solution[i, j] = Random.Range(0, 3);
                //Debug.Log(solution[i, j]);
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
        repairCount = 0.0f;
        repairSlider.value = 0.0f;
    }

    void AdvanceProgress() {
        ++setIndex;
        if(setIndex > 3) {
            setIndex = 0;
            ++solutionIndex;
            if(solutionIndex > 3) {
                solutionIndex = 0;
                isSolved = true;
            }
        }
    }

    // Determines if the player is in range
    void TrackPlayer() {
        float playerDistance = (player.transform.position - transform.position).sqrMagnitude;
        if(playerDistance < playerRange) {
            isPlayerInRange = true;
            if (!repairSlider.gameObject.activeSelf) {
                repairSlider.gameObject.SetActive(true);
            }
        } else {
            isPlayerInRange = false;
            if (repairSlider.gameObject.activeSelf) {
                ResetProgress();
                repairSlider.gameObject.SetActive(false);
            }
        }
    }

    public bool IsSolRepairing() {
        return isAISolving && (player.transform.position - transform.position).sqrMagnitude <= playerRange && !isSolved;
    }

    public bool IsSolved() {
        return isSolved;
    }

    public void DrainRepair(float _fDamage) {
        if (isSolved) {
            return;
        }
        repairCount -= _fDamage;
        if (repairCount < 0) {
            repairCount = 0.0f;
        }
        repairSlider.value = repairCount;
        
    }

    void TurnOnLights() {
        // Find sector game object
        GameObject sector = GameObject.Find(sectorName);
        // Iterate through children and turn on emission
        //sector.transform.chi
    }

}
