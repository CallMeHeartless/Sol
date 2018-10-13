using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    static AudioController instance;

    private AudioSource[] sounds;

	// Use this for initialization
	void Start () {
        instance = this;
        sounds = GetComponents<AudioSource>();
        foreach(AudioSource sound in sounds) {
            Debug.Log(sound.clip.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    // Plays a single sound
    public static void PlaySingleSound(string sound) {
        AudioSource audio = instance.GetSource(sound);
        if (audio) {
            if (!audio.isPlaying) {// Stop looping sounds from starting up again
                audio.Play();
            }
        } else {
            Debug.Log("Sound not found - null reference.");
        }
    }

    // Stops a sound (assumed to be looping)
    public static void StopSingleSound(string sound) {
        AudioSource audio = instance.GetSource(sound);
        if (audio) {
            audio.Play();
        } else {
            Debug.Log("Sound not found - null reference.");
        }
    }

    private AudioSource GetSource(string sound) {
        foreach(AudioSource audio in sounds) {
            if(audio.clip.name == sound) {
                return audio;
            }
        }

        return null;
    }
}
