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
    public bool bCanAttack = true;
    public float fAttackRadius = 5.0f;
    public bool bChase = false;
    public bool bWave = false;

    public Collider[] colliders;
    public Collider[] MeatHunks;

    // Audio
    private AudioSource[] enemyAudio;
    bool breathOne = true;
    
    public void Dead()
    {
        if(!isAlive && agent.enabled)
        {
            foreach(Collider coll in colliders)
            {
                coll.enabled = false;
            }
            agent.isStopped = true;
            agent.enabled = false;

            // Play death effect
            int iRandom = Random.Range(0, 2);
            if (iRandom == 0) {
                enemyAudio[2].Play();
            } else {
                enemyAudio[3].Play();
            }
        }
    }

    public void enableColliders()
    {
        foreach(Collider coll in MeatHunks)
        {
            coll.enabled = true;
        }
    }

    public void disableColliders()
    {
        foreach (Collider coll in MeatHunks)
        {
            coll.enabled = false;
        }
    }

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
        if(agent.enabled)
        {
            bIsAttacking = true;
            // Animation
            anim.SetTrigger("Attack");
            agent.isStopped = true;
            // cooldown
            StartCoroutine(AttackCooldown(fAttackRate));
        }
    }

    IEnumerator AttackCooldown(float _fAttackCooldown)
    {
        yield return new WaitForSeconds(_fAttackCooldown);
        if(agent.enabled)
        {
            agent.isStopped = false;
            anim.SetTrigger("Run");
            bIsAttacking = false;
            bCanAttack = true;
        }
        

    }

    void AttackDistance()
    {

        if(bChase && agent.enabled)
        {
            fDistance = (player.transform.position - transform.position).magnitude;

            if (fDistance < fAttackRadius)
            {
                if (bCanAttack == true && isAlive == true)
                {
                    Attack();
                    bCanAttack = false;
                }

            }
        }
        
    }

    public void movement()
    {
        if(bChase && agent.enabled)
        {
            if(agent.isStopped == false)
            {
                anim.SetTrigger("Run");
            }

            Vector3 vA = player.transform.position;
            vA.y = vA.y + 3.0f;

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
        enemyAudio = GetComponents<AudioSource>();
        if(enemyAudio == null) {
            Debug.Log("ERROR: Missing Audio for enemy object.");
        } else {
            //StartCoroutine(EnemyBreathing());
            InvokeRepeating("EnemyBreathing2", 0, 1.5f);
        }
    }
	
	// Update is called once per frame
	void Update () {

        movement();
        AttackDistance();
        Dead();
    }

    public void DamageEnemy(int _iDamage) {
        enemyAudio[4].Play();
        m_iLife -= m_iLife;
        if(m_iLife <= 0 && isAlive) {
            isAlive = false;
            agent.isStopped = true;
            Dead();
            // Cue death animation
            anim.SetTrigger("Die");
        }// If the enemy was hit from stealth, make them chase the player
        else if(isAlive && !bChase) {
            bChase = true;
        }
    }

    IEnumerator EnemyBreathing() {
        yield return new WaitForSeconds(1.5f);
        if (!bChase) {
            // Switch between playing audio
            if (!enemyAudio[0].isPlaying && !enemyAudio[1].isPlaying) {
                if (breathOne) {
                    enemyAudio[0].Play();
                    Debug.Log("BreathOne");
                } else {
                    enemyAudio[1].Play();
                    Debug.Log("BreathTwo");
                }
                
                breathOne = !breathOne;
                StartCoroutine(EnemyBreathing());
            }
        }

    }

    void EnemyBreathing2() {
        if (!isAlive) {
            return;
        }
        if (!bChase) {
            // Switch between playing audio
            if (!enemyAudio[0].isPlaying && !enemyAudio[1].isPlaying) {
                if (breathOne) {
                    enemyAudio[0].Play();
                    Debug.Log("BreathOne");
                } else {
                    enemyAudio[1].Play();
                    Debug.Log("BreathTwo");
                }

                breathOne = !breathOne;
                StartCoroutine(EnemyBreathing());
            }
        } else {
            // Audio for player being chased
            if (!enemyAudio[6].isPlaying && !enemyAudio[7].isPlaying) {
                if (breathOne) {
                    enemyAudio[6].Play();
                } else {
                    enemyAudio[7].Play();
                }

                breathOne = !breathOne;
                StartCoroutine(EnemyBreathing());
            }
        }
    }
}
