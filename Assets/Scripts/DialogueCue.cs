using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCue : MonoBehaviour {

    public string Message;
    public float duration = 5.0f;
    public bool triggerOnce = true;
    private bool isTriggered = false;

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !isTriggered) {
            if (triggerOnce) {
                isTriggered = true;
            }
            DialogueController.BasicMessage(Message, duration);
        }
    }

}
