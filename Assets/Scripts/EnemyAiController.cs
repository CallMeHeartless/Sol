using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiController : MonoBehaviour {

    [SerializeField]
    private int m_iLife = 1;

    private bool isAlive = true;

    public GameObject player;
    public NavMeshAgent agent;

    private Ray ray;
    public Animator anim;
    private float fDistance;

    private bool bIsStunned = false;
    public bool bIsAttacking = false;
    private float fAttackRate = 0.6f;
    private bool bCanAttack = true;
    public float fAttackRadius = 5.0f;
    public bool bChase = false;
    public bool bWave = false;
    

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

    private void WaveEnemy()
    {
        if(bWave == true)
        {
            bChase = true;
            anim.SetTrigger("Run");
            // Determine target
            int random = Random.Range(0, 4);
            if(random == 0) {
                // Target Sol
                player = GameObject.Find("Sol");
            }

        }
    }

    private void Attack()
    {
        bIsAttacking = true;
        // Animation
        anim.SetTrigger("Attack");
        agent.isStopped = true;
        // cooldown
        StartCoroutine(AttackCooldown(fAttackRate));
    }

    IEnumerator AttackCooldown(float _fAttackCooldown)
    {
        yield return new WaitForSeconds(_fAttackCooldown);
        agent.isStopped = false;
        anim.SetTrigger("Run");
        bIsAttacking = false;

    }

    void AttackDistance()
    {

        if(bChase)
        {
            fDistance = (player.transform.position - transform.position).magnitude;

            if (fDistance < fAttackRadius)
            {
                if (bCanAttack == true && isAlive == true)
                {
                    Attack();
                }

            }
        }
        
    }

    public void movement()
    {
        if(bChase)
        {
            if(agent.isStopped == false)
            {
                anim.SetTrigger("Run");
            }

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
    }

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");//FindPlayer();
        anim = GetComponent<Animator>();
        anim.SetTrigger("Idle");
        WaveEnemy();
    }
	
	// Update is called once per frame
	void Update () {

        movement();
        AttackDistance();

    }

    public void DamageEnemy(int _iDamage) {
        m_iLife -= m_iLife;
        if(m_iLife <= 0 && isAlive) {
            isAlive = false;
            agent.isStopped = true;
            // Cue death animation
            anim.SetTrigger("Die");
        }// If the enemy was hit from stealth, make them chase the player
        else if(isAlive && !bChase) {
            bChase = true;
        }
    }
}
