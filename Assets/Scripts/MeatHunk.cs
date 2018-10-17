using System.Collections;
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
                other.GetComponent<PlayerController>().DrainCharge(fDam);
                
            }
            else if(other.CompareTag("Sol"))
            {
                GameObject gen = other.GetComponent<AiController>().Generator;
                if(gen != null) {
                    gen.GetComponent<GeneratorPuzzleController>().DrainRepair(fDam / 2);
                }
            }
        }
    }
}
