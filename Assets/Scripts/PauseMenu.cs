using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject controlMenuUI;
    public Text Instructions;
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        controlMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu");
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }


    public void Start() {
        // Display instructions
        Instructions.text = "Stay with Sol to keep yourself charged. Your charge drains when you are away from Sol, and you die when you run out!";
        StartCoroutine(FadeInstructions());

    }

    IEnumerator FadeInstructions() {
        yield return new WaitForSeconds(5.0f);
        Instructions.gameObject.SetActive(false);

    }
}
