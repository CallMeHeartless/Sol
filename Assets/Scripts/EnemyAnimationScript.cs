using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationScript : MonoBehaviour {

    public GameObject parent;

	IEnumerator Death() {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
