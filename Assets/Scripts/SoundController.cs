using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {
    public static SoundController instance;
    AudioSource source;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
        source = GetComponent<AudioSource>();
    }
    

    public void Play(string soundName)
    {
        var sound = Resources.Load<AudioClip>("Sound/" + soundName);
        source.clip = sound;
        source.Play();
    }
}
