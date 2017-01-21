using UnityEngine;
using System.Collections;

public class PlaySoundExample : MonoBehaviour {

    public GameObject soundPrefab; // plut your sound prefab here

    private GameObject soundInstance;
    private FullIndieAudioSound sound;

    public bool retrigger = false;
    public float retriggerThreshhold = 0.2f;

    public bool stop = false;

    void Start () {
        // to load a prefab you can instanciate it
        // this returns a Object which needs to be cast as a GameObject
        soundInstance = GameObject.Instantiate(soundPrefab, Vector3.one, Quaternion.identity) as GameObject;
        sound = soundInstance.GetComponent<FullIndieAudioSound>();
        // if we store a FullIndieAudioSound as a reference we can call the Play() method on it
        sound.Play();
	}

    float timer = 0;
    void Update()
    {
        if(sound != null)
        {
            timer += Time.deltaTime;
            if(retrigger && timer > retriggerThreshhold)
            {
                timer = 0;
                sound.Play();
            }
        }

        if(stop)
        {
            sound.StopAll();
            stop = false;
        }
    }
}
