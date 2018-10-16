using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVision : MonoBehaviour {

    private Ray ray;
    public GameObject player;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {

            GetComponentInParent<EnemyAiController>().bChase = true;

            if(GetComponentInParent<EnemyAiController>().agent.enabled)
            {
                GetComponentInParent<EnemyAiController>().agent.isStopped = false;
            }
            GetComponentInParent<EnemyAiController>().anim.SetTrigger("Run");
            Destroy(GetComponent<CapsuleCollider>());

            /*
            Debug.Log("Whut?");

            Vector3 raydir = (transform.position - player.transform.position);

            ray.origin = transform.parent.position;
            ray.direction = raydir;

            RaycastHit hit;

            Debug.DrawRay(transform.parent.position, raydir, Color.green, 100.0f, false);

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider == other.GetComponent<CapsuleCollider>())
                {
                    GetComponentInParent<EnemyAiController>().bChase = true;
                    GetComponentInParent<EnemyAiController>().agent.isStopped = false;
                    gameObject.SetActive(false);
                }
            }
            */
        }
    }
}
