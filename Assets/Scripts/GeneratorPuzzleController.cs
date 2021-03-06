﻿using System.Collections;
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
    private AudioSource runningNoise;

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
        runningNoise = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (isSolved) {
            return;
        }

        // Check player is in range to solve puzzle
       // TrackPlayer();

        // AI solving
        if(isPlayerInRange && isAISolving) {
            repairCount += Time.deltaTime;
            repairSlider.value = repairCount;
            if(repairCount >= repairCountdown) {

                MakeRepaired();
            }
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            TurnOnLights();
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
            AudioController.PlaySingleSound("ALARM_Submarine_Slow_loop_stereo");
            AudioController.PlaySingleSound("COMPUTER_Sci-Fi_Processing_01_loop_mono");
            if (!repairSlider.gameObject.activeSelf) {
                repairSlider.gameObject.SetActive(true);
            }
        } else {
            AudioController.StopSingleSound("ALARM_Submarine_Slow_loop_stereo");
            AudioController.StopSingleSound("COMPUTER_Sci-Fi_Processing_01_loop_mono");
            isPlayerInRange = false;
            if (repairSlider.gameObject.activeSelf) {
                ResetProgress();
                repairSlider.gameObject.SetActive(false);
            }
        }
    }

    public bool IsSolRepairing() {
        return isAISolving && isPlayerInRange && !isSolved;
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
        Renderer[] materials = sector.GetComponentsInChildren<Renderer>();

        foreach(Renderer renderer in materials) {

            if (CheckForEmissiveMaterial(renderer.material)) {
                renderer.material.EnableKeyword("_EMISSION");
            } 
        }

        Light[] lights = sector.GetComponentsInChildren<Light>(true);

        foreach(Light light in lights) {
            light.enabled = true;
        }
    }

    // Checks if a specified material is one of the emissive materials
    bool CheckForEmissiveMaterial(Material _test) {
        if(_test.name == "M_Ceiling_Light (Instance)"||
           _test.name == "M_FloorEdge_InnerCorner (Instance)" ||
           _test.name == "M_FloorEdge_Straight (Instance)" ||
           _test.name == "M_Wall_InnerCorner01 (Instance)"||
           _test.name == "M_Wall_InnerCorner02 (Instance)"||
           _test.name == "M_Wall_InnerCorner03 (Instance)"||
           _test.name == "M_Wall_InnerCorner04 (Instance)"||
           _test.name == "M_Wall_OuterCorner01 (Instance)"||
           _test.name == "M_Wall_OuterCorner02 (Instance)"||
           _test.name == "M_Wall_OuterCorner03 (Instance)" ||
           _test.name == "M_Wall_OuterCorner04 (Instance)" ||
           _test.name == "M_Wall_Straight01 (Instance)" ||
           _test.name == "M_Wall_Straight02 (Instance)" ||
           _test.name == "M_Wall_Straight03 (Instance)" ||
           _test.name == "M_Wall_Straight04 (Instance)" 
           //||
           //_test.name == "M_Wall_Pillar01 (Instance)" ||
           //_test.name == "M_Wall_Pillar02 (Instance)" ||
           //_test.name == "M_Wall_Pillar03 (Instance)" ||
           //_test.name == "M_Wall_Pillar04 (Instance)"
           ) {
            return true;
        }

        return false;
    }

    void MakeRepaired() {
        isSolved = true;
        Debug.Log("Repaired");
        AudioController.StopSingleSound("ALARM_Submarine_Slow_loop_stereo");
        AudioController.StopSingleSound("COMPUTER_Sci-Fi_Processing_01_loop_mono");
        TurnOnLights();
        runningNoise.Play();
        GameManager.MarkFuseBoxAsRepaired();
        GameObject.Find("Player").GetComponent<PlayerController>().GiveCharge(100);
        int iRandom = Random.Range(0, 2);
        if (iRandom == 0) {
            DialogueController.BasicMessage("We're done here, let's move forwards.", 4.0f);
        } else {
            DialogueController.BasicMessage("One more generator restored. I'm beginning to feel better already.", 4.0f);
        }
        //GameObject.Find("Sol").GetComponent<AiController>().StopRepair();
    }

    public void OnTriggerEnter(Collider other) {
        if (isSolved) {
            return;
        }
        if (other.CompareTag("Sol")) {
            isPlayerInRange = true;
            //other.GetComponent<AiController>().StartRepair();
            AudioController.PlaySingleSound("ALARM_Submarine_Slow_loop_stereo");
            AudioController.PlaySingleSound("COMPUTER_Sci-Fi_Processing_01_loop_mono");
            if (!repairSlider.gameObject.activeSelf) {
                repairSlider.gameObject.SetActive(true);
            }
        }
    }

    public void OnTriggerExit(Collider other) {
        if (isSolved) {
            return;
        }
        if (other.CompareTag("Sol")) {
            //other.GetComponent<AiController>().StopRepair();
            AudioController.StopSingleSound("ALARM_Submarine_Slow_loop_stereo");
            AudioController.StopSingleSound("COMPUTER_Sci-Fi_Processing_01_loop_mono");
            isPlayerInRange = false;
            if (repairSlider.gameObject.activeSelf) {
                ResetProgress();
                repairSlider.gameObject.SetActive(false);
            }
        }
    }

}
