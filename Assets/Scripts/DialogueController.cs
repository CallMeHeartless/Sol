using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {

    private static DialogueController instance;
    GameObject dialoguePanel;
    Text dialogueText;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Displays a basic message to the dialogue window for a given duration
    public static void BasicMessage(string _message, float _duration) {
        instance.dialoguePanel.SetActive(true);
        instance.dialogueText.text = _message;
    }

    IEnumerator ClearDialogue(float _delay) {
        yield return new WaitForSeconds(_delay);
        instance.dialogueText.text = "";
        instance.dialoguePanel.SetActive(false);
    }
}
