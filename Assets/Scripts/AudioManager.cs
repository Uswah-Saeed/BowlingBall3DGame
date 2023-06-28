using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;


    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        foreach (Sounds sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.loop = sound.loop;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
        }
    }
    public void PlayMySound(string name)
    {
        foreach (Sounds sound in sounds)
        {
            if (sound.name == name)
            {
                sound.audioSource.Play();
            }

        }
    }
}
