using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESpawn : MonoBehaviour {

    public string name;

	// Use this for initialization
	void Start () {
		if (name == "")
        {
            name = "Spawn1";
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
