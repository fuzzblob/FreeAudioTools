using UnityEngine;
using System.Collections;

public class PlaySoundExample : MonoBehaviour {

    public GameObject soundPrefab; // plut your sound prefab here

    private GameObject soundInstance;
    private FullIndieAudioSound sound;

    void Start () {
        // to load a prefab you can instanciate it
        // this returns a Object which needs to be cast as a GameObject
        soundInstance = GameObject.Instantiate(soundPrefab, Vector3.one, Quaternion.identity) as GameObject;
        sound = soundInstance.GetComponent<FullIndieAudioSound>();
        // if we store a FullIndieAudioSound as a reference we can call the Play() method on it
        sound.Play();
	}
}
