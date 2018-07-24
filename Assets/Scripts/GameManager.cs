using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject[] powerBoxes;
    public Light directionalLight;
    public static Text fuseBoxProgressText;

    bool gameOver = false;
    static int totalFuseBoxes;
    static int fuseBoxesRepaired = 0;

	// Use this for initialization
	void Start () {
        //player = player.GetComponent<PlayerController>();
        int totalFuseBoxes = powerBoxes.Length;
        fuseBoxProgressText = GetComponentInChildren<Text>();
        fuseBoxProgressText.text = "Fuse Boxes repaired: " + fuseBoxesRepaired.ToString() + " / " + totalFuseBoxes.ToString();
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

    public static void MarkFuseBoxAsRepaired() {
        ++fuseBoxesRepaired;
        fuseBoxProgressText.text = "Fuse Boxes repaired: " + fuseBoxesRepaired.ToString() + " / " + totalFuseBoxes.ToString();
    }
}
