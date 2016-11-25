using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class FullIndieAudioSound : MonoBehaviour {

    #region Properties
    public bool playOnAwake = false;

    [Range(-80, 0)]
    public float volume = 0; // value in deci Bell (dB); convert to linear via FullIndieAudioUtil
    [Range(-12, 0)]
    public float randomVolume = 0;
    [Range(-24, 24)]
    public float pitch = 0; // value in semitones; convert to log float via FullIndieAudioUtil
    [Range(0, 12)]
    public float randomPitch = 0;

    public bool retrigger = false;
    [Range(0,500)]
    public int triggerRate = 12;
    [Range(0,500)]
    public int randomTriggerStart = 5;
    public bool looping = false;

    [Range(10,22000)]
    public float lowPassFilter = 22000;
    [Range(10,22000)]
    public float highPassFilter = 10;
    
    public bool spacial = true;
    public bool vr = false;
    [Range(0,500)]
    public float mindDistance = 1;
    [Range(1.1f, 500)]
    public float maxDistance = 100;
    public AudioRolloffMode rolloff = AudioRolloffMode.Logarithmic;


    public List<AudioClip> clips = new List<AudioClip>();
    #endregion

    #region State
    [System.NonSerialized]
    public List<AudioSource> sources = new List<AudioSource>();
    [System.NonSerialized]
    AudioLowPassFilter lpf = null;
    [System.NonSerialized]
    AudioHighPassFilter hpf = null;
    [System.NonSerialized]
    bool playing;
    [System.NonSerialized]
    int lastIndex = -1;
    [System.NonSerialized]
    uint updateCounter;
    #endregion

    void Start () {
        sources.Add(transform.GetComponent<AudioSource>());
        lpf = transform.GetComponent<AudioLowPassFilter>();
        hpf = transform.GetComponent<AudioHighPassFilter>();
    }
    void Awake()
    {
        if(playOnAwake)
        {
            Play();
        }
    }
    
    void LateUpdate()
    {
        if (retrigger && playing) // only happens when initial play and not stopped
        {
            updateCounter++;
            if (updateCounter > triggerRate)
            {
                updateCounter = (uint)Random.Range(0, randomTriggerStart);
                Play();
            }
        }
    }

    public void Play()
    {
        Start();
        AudioSource source = GetSource();
        playing = true;

        source.clip = GetRandomSound();
        source.volume = GetRandomVolume();
        source.pitch = GetRandomPitch();

        if (retrigger == false)
        {
            if (looping) { source.loop = true; }
        }
        if (spacial) { source.spatialBlend = 1f; }
        else { source.spatialBlend = 0f; }
        if (vr) { source.spatialize = true; }

        if (lowPassFilter < 22000)
        {
            lpf.enabled = true;
            lpf.cutoffFrequency = lowPassFilter;
        }
        else
        {
            if (lpf != null) { lpf.enabled = false; }
        }
        if (highPassFilter > 10)
        {
            hpf.enabled = true;
            hpf.cutoffFrequency = highPassFilter;
        }
        else
        {
            if (hpf != null) { hpf.enabled = false; }
        }

        if (source.volume > 0 || source.pitch == 0) // prevent waisting an audio source if it's silent
        {
            source.Play();
        }
    }
    public void StopAll()
    {
        playing = false;
        for(int i = 0; i < sources.Count; i++)
        {
            sources[i].Stop();
        }
    }

    private AudioSource GetSource()
    {
        for(int i = 0; i < sources.Count; i++)
        {
            if(sources[i].isPlaying == false)
            {
                return sources[i]; // exit loop with found source
            }
        }
        AudioSource result = gameObject.AddComponent<AudioSource>(); // if all sources are playing, add a new one
        sources.Add(result);
        return result;
    }
    private AudioClip GetRandomSound()
    {
        int index;
        if (clips.Count > 2) // prevents flip floppin
        {
            index = lastIndex;
            while (index == lastIndex) // this makes sure the sounds don't repeat if there is variation available
            {
                index = Random.Range(0, clips.Count - 1);
            }
            lastIndex = index;
        }
        else if(clips.Count == 2)
        {
            index = Random.Range(0, clips.Count - 1);
            lastIndex = index;
        }
        else
        {
            index = 0;
        }
        return clips[index];
    }
    private float GetRandomVolume()
    {
        return FullIndieAudioUtil.DecibelToLinear(volume + Random.Range(randomVolume, 0));
    }
    private float GetRandomPitch()
    {
        return FullIndieAudioUtil.St2pitch(pitch + Random.Range(0, randomPitch));
    }
}
