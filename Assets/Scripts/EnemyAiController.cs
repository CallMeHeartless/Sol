using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiController : MonoBehaviour {

    [SerializeField]
    private int m_iLife = 5;

    private bool isAlive = true;

    public GameObject player;
    public NavMeshAgent agent;

    private Ray ray;
    private Animator anim;
    private float fDistance;

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

    public void movement()
    {
        Vector3 vA = player.transform.position;
        vA.y = vA.y + 4.0f;

        ray.origin = vA;
        ray.direction = Vector3.down;

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            agent.SetDestination(hit.point);
        }
    }

    // Use this for initialization
    void Start () {
        player = FindPlayer();
	}
	
	// Update is called once per frame
	void Update () {

        movement();

    }

    public void DamageEnemy(int _iDamage) {
        m_iLife -= m_iLife;
        if(m_iLife <= 0) {
            isAlive = false;
        }
    }
}
