using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    static AudioController instance;

    public enum ESOUND {

    }

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void PlaySingleSound(ESOUND sound) {
        switch (sound) {
            
        }
    }
}
