using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatHunk : MonoBehaviour {

    public float fDam = 5.0f;
    private Collider collider;

	// Use this for initialization
	void Start () {
        collider = GetComponent<Collider>();
        collider.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponentInParent<EnemyAiController>().bIsAttacking == true)
        {
            collider.enabled = true;
            //Debug.Log("Enable the box collider");
        }
        else
        {
            collider.enabled = false;
            //Debug.Log("Disable box collider");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<PlayerController>().DrainCharge(fDam);
            Debug.Log("Huh");
            collider.enabled = false;
            GetComponentInParent<EnemyAiController>().bIsAttacking = false;
        }
    }
}
