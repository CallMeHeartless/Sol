using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBoxController : MonoBehaviour {

    public bool isFixed = false;
    public float maxHealth = 4.0f;
    public GameObject player;
    public GameObject[] spawnLocations;

    float fixHealth = 0.0f;
    float repairDistance = 2.0f;
    float playerDistance;

    public Slider fixSlider;
    

	// Use this for initialization
	void Start () {
        int spawnIndex = Random.Range(0, spawnLocations.Length);
        transform.position = spawnLocations[spawnIndex].transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        // Check distance to player
        playerDistance = (player.transform.position - transform.position).magnitude;
        if(playerDistance <= repairDistance) {
            // Show repair bar
            //fixSlider.enabled = true;
            fixSlider.gameObject.SetActive(true);
            // Check if the player is holding repair 
            if (Input.GetKey(KeyCode.E) && !isFixed) {
                fixHealth += Time.deltaTime;
                fixSlider.value = fixHealth;
                if(fixHealth >= maxHealth) {
                    isFixed = true;
                }
            }


        }else if (fixSlider.enabled) {
            fixSlider.gameObject.SetActive(false);
        }

    }
}
