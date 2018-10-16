using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatHunk : MonoBehaviour {

    public float fDam = 5.0f;
    public Collider collider;

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
        if (other.CompareTag("Player") || other.CompareTag("Sol"))
        {
            if(other.CompareTag("Player"))
            {
                Debug.Log("Huh");
                other.GetComponent<PlayerController>().DrainCharge(fDam);
                collider.enabled = false;
                GetComponentInParent<EnemyAiController>().bIsAttacking = false;
            }
            else if(other.CompareTag("Sol"))
            {
                Debug.Log("Other Huh");
                collider.enabled = false;
                other.GetComponent<AiController>().Generator.GetComponent<GeneratorPuzzleController>().DrainRepair(fDam / 2);
                GetComponentInParent<EnemyAiController>().bIsAttacking = false;
            }
        }
    }
}
