using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundM { get; private set; }

    private void Awake()
    {
        if (soundM == null)
        {
            soundM = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioMixer audioMixer;
    public float volume;

    private void Start()
    {
        SetVolume(volume);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        this.volume = volume;
    }

    public float GetVolume()
    {
        return volume;
    }
}
