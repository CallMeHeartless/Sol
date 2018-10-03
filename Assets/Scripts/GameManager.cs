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
        Cursor.visible = false;
        PlayerController.MakeAlive(); // Enforces that the player is alive
        totalFuseBoxes = powerBoxes.Length;
        //Debug.Log(totalFuseBoxes);
        fuseBoxProgressText = GetComponentInChildren<Text>();
        fuseBoxProgressText.text = "Fuse Boxes Repaired: " + fuseBoxesRepaired.ToString() + " / " + totalFuseBoxes.ToString();
        SpawnEnemies();
        
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(totalFuseBoxes);
        // Check for game over
        if (CheckForVictory() && !gameOver) {
            gameOver = true;
            directionalLight.GetComponent<Light>().intensity = 1.1f;
            // Play player animation
            GameObject.Find("Player").GetComponent<Animator>().SetBool("PlayerWins", true);
            // Start couroutine to end game
            gameOverMenu.GetComponentInChildren<Text>().text = "Congratulations! You win!";
            
            StartCoroutine(GameOverMenu());
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
        //Debug.Log(totalFuseBoxes);
    }

    public static void SpawnEnemies()
    {
        GameObject[] gos;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        gos = GameObject.FindGameObjectsWithTag("ESpawn");
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

        string groupName = closest.GetComponent<ESpawn>().name;

        foreach (GameObject go in gos)
        {
            if (groupName == go.GetComponent<ESpawn>().name)
            {
                GameObject Apebyss = Instantiate(Resources.Load("Apebyss", typeof(GameObject))) as GameObject;
                Apebyss.transform.position = go.transform.position;
            }
        }
    }
}
