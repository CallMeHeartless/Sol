using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBoxController : MonoBehaviour {

    public bool isFixed = false;
    public float maxHealth = 4.0f;
    public GameObject player;

    float fixHealth = 0.0f;
    float repairDistance = 0.5f;
    float playerDistance;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Check distance to player
        playerDistance = (player.transform.position - transform.position).magnitude;
        if(playerDistance <= repairDistance) {
            // Show repair bar

            // Check if the player is holding repair 
            if (Input.GetKeyDown(KeyCode.E) && !isFixed) {
                fixHealth += Time.deltaTime;
                if(fixHealth >= maxHealth) {
                    isFixed = true;
                }
            }


        }

    }
}
