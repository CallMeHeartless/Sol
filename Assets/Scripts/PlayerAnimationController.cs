using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayerAttack() {
        // Make player attack
        player.FireWeapon();
    }
}
