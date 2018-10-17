﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatHunk : MonoBehaviour {

    public float fDam = 2.0f;
    public Collider collider;

	// Use this for initialization
	void Start () {
        collider = GetComponent<Collider>();
        collider.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Sol"))
        {
            if(other.CompareTag("Player"))
            {
                Debug.Log("Huh");
                other.GetComponent<PlayerController>().DrainCharge(fDam);
                
            }
            else if(other.CompareTag("Sol"))
            {
                Debug.Log("Other Huh");
                
                other.GetComponent<AiController>().Generator.GetComponent<GeneratorPuzzleController>().DrainRepair(fDam / 2);
                
            }
        }
    }
}
