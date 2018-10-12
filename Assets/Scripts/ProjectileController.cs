using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float fSpeed = 10.0f;
    public int iDamage = 10;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * fSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            // Damage
            other.GetComponent<EnemyAiController>().DamageEnemy(iDamage);
        }
        Destroy(gameObject);
    }
}
