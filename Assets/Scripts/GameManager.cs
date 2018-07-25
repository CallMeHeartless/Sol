using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject[] powerBoxes;
    public Light directionalLight;
    public Text fuseBoxProgressText;
    public GameObject gameOverMenu;

    bool gameOver = false;
    int totalFuseBoxes;
    int fuseBoxesRepaired = 0;

	// Use this for initialization
	void Start () {
        PlayerController.MakeAlive(); // Enforces that the player is alive
        totalFuseBoxes = powerBoxes.Length;
        //Debug.Log(totalFuseBoxes);
        fuseBoxProgressText = GetComponentInChildren<Text>();
        fuseBoxProgressText.text = "Fuse Boxes Repaired: " + fuseBoxesRepaired.ToString() + " / " + totalFuseBoxes.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(totalFuseBoxes);
        // Check for game over
        if (CheckForVictory() && !gameOver) {
            gameOver = true;
            directionalLight.GetComponent<Light>().intensity = 1.1f;
            // Start couroutine to end game
        }

        if (!PlayerController.IsAlive() && !gameOver) {
            //Debug.Log("DEAD");
            // Game over stuff
            gameOver = true;
            StartCoroutine(GameOverMenu());
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
        fuseBoxProgressText.text = "Fuse Boxes Repaired: " + fuseBoxesRepaired.ToString() + " / " + totalFuseBoxes.ToString();
        Debug.Log(totalFuseBoxes);
    }
}
