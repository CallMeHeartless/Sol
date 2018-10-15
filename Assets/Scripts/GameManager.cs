﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    private GameObject[] generators;
    public Text fuseBoxProgressText;
    public GameObject gameOverMenu;
    public GameObject elevatorDoor;
    private GameObject player;
    private GameObject[] enemySpawn;

    bool gameOver = false;
    int totalGenerators;
    int fuseBoxesRepaired = 0;

    static public float fWaveMaxTime = 5.0f;
    static float fWaveTime = 0.0f;

	// Use this for initialization
	void Start () {
        instance = this;
        Cursor.visible = false;
        PlayerController.MakeAlive(); // Enforces that the player is alive
        generators = GameObject.FindGameObjectsWithTag("Generator");
        totalGenerators = generators.Length;
        //Debug.Log(totalFuseBoxes);
        fuseBoxProgressText = GetComponentInChildren<Text>();
        fuseBoxProgressText.text = "Generators Repaired: " + fuseBoxesRepaired.ToString() + " / " + totalGenerators.ToString();
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null) {
            Debug.Log("ERROR: PLAYER NOT FOUND!");
        }
        enemySpawn = GameObject.FindGameObjectsWithTag("ESpawn");
        
        
    }
	
	// Update is called once per frame
	void Update () {
        SpawnWaves();

        // Check for game over
        if (CheckForVictory() && !gameOver) {
            gameOver = true;
            if(elevatorDoor != null) {
                elevatorDoor.GetComponent<DoorController>().Unlock();
            } else {
                Debug.Log("ERROR: Elevator door is null reference.");
            }

        }else if (!PlayerController.IsAlive() && !gameOver) {
            // Game over stuff
            gameOver = true;
            StartCoroutine(GameOverMenu());
        }

	}

    // Iterates through all powerboxes and checkes if they have all been fixed
    public static bool CheckForVictory() {
        foreach(GameObject fuseBox in instance.generators) {
            if (!fuseBox.GetComponent<GeneratorPuzzleController>().IsSolved()) {
                return false;
            }
        }

        return true;
    }

    IEnumerator GameOverMenu() {
        yield return new WaitForSeconds(4.5f);
        // MENU STUFF HERE
        gameOverMenu.SetActive(true);
        yield return null;
    }

    IEnumerator ReloadLevel() {
        yield return new WaitForSeconds(2.5f);

        gameOver = false;
        SceneManager.LoadScene(1);
        PlayerController.MakeAlive();
        yield return null;
    }

    public void MarkFuseBoxAsRepaired() {
        ++fuseBoxesRepaired;
        fuseBoxProgressText.text = "Fuse Boxes Repaired: " + fuseBoxesRepaired.ToString() + " / " + totalGenerators.ToString();
        //Debug.Log(totalFuseBoxes);
    }

    public static void SpawnEnemies()
    {
        //GameObject[] gos;
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //gos = GameObject.FindGameObjectsWithTag("ESpawn");
        GameObject closest = null;
        float distance = 100000.0f;
        Vector3 position = instance.player.transform.position;
        foreach (GameObject go in instance.enemySpawn)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        if(closest != null) {
            string groupName = closest.GetComponent<ESpawn>().name;

            foreach (GameObject go in instance.enemySpawn) {
                if (groupName == go.GetComponent<ESpawn>().name) {
                    GameObject Apebyss = Instantiate(Resources.Load("Apebyss", typeof(GameObject))) as GameObject;
                    Apebyss.transform.position = go.transform.position;
                    Apebyss.GetComponent<EnemyAiController>().bWave = true;
                }
            }
        }

    }

    public static void SpawnWaves()
    {
        GameObject[] gos;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        gos = GameObject.FindGameObjectsWithTag("Generator");
        GameObject closest = null;
        float distance = 100000.0f;
        Vector3 position = player.transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        if(closest != null) {
            if (closest.GetComponent<GeneratorPuzzleController>().IsSolRepairing() == true) {
                if (fWaveTime > fWaveMaxTime) {
                    SpawnEnemies();
                    fWaveTime = 0.0f;
                } else {
                    fWaveTime = fWaveTime + Time.deltaTime;
                }
            }
        }
    }

    // Loads the next level in build settings
    public static void LoadNextLevel() {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex+1 < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(currentIndex + 1);
        } else {
            // return to main menu
            SceneManager.LoadScene(0);
            Cursor.visible = true;
        }
        
    }
}
