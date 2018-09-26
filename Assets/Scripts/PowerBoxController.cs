using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBoxController : MonoBehaviour {

    public bool isFixed = false;
    public float maxHealth = 4.0f;
    public GameObject player;
    public GameObject[] spawnLocations;
    public GameObject brokenMesh;
    public GameObject fixedMesh;

    float fixHealth = 0.0f;
    float repairDistance = 2.0f;
    float playerDistance;

    public Slider fixSlider;

    public GameObject FindPlayer()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = 100000.0f;
        Vector3 position = transform.position;
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
        return closest;
    }

    // Use this for initialization
    void Start () {
        if(spawnLocations != null) {
            int spawnIndex = Random.Range(0, spawnLocations.Length);
            transform.position = spawnLocations[spawnIndex].transform.position;
            transform.rotation = spawnLocations[spawnIndex].transform.rotation;
        }

        player = FindPlayer();
	}
	
	// Update is called once per frame
	void Update () {
        // Check distance to player
        if(player != null) {
            playerDistance = (player.transform.position - transform.position).magnitude;
            if (playerDistance <= repairDistance) {
                // Show repair bar
                //fixSlider.enabled = true;
                fixSlider.gameObject.SetActive(true);
                // Check if the player is holding repair 
                if (Input.GetKey(KeyCode.E) && !isFixed) {
                    fixHealth += Time.deltaTime;
                    fixSlider.value = fixHealth;
                    if (fixHealth >= maxHealth) {
                        isFixed = true;
                        GameObject.Find("GameManager").GetComponent<GameManager>().MarkFuseBoxAsRepaired();
                        // Change mesh
                        brokenMesh.SetActive(false);
                        fixedMesh.SetActive(true);
                    }
                }
            }



        }else if (fixSlider.enabled) {
            fixSlider.gameObject.SetActive(false);
            
        }

    }
}
