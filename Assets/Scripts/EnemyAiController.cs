using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiController : MonoBehaviour {

    [SerializeField]
    private int m_iLife = 5;

    private bool isAlive = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DamageEnemy(int _iDamage) {
        m_iLife -= m_iLife;
        if(m_iLife <= 0) {
            isAlive = false;
            // Cue death animation
        }
    }
}
