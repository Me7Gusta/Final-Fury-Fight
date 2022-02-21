using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip levelSong, bossSong, gameOverSong;

    private AudioSource audioS;
    private float volume;

    private void Start()
    {
        volume = SoundManager.soundM.GetVolume();
        SoundManager.soundM.SetVolume(volume);
        audioS = GetComponent<AudioSource>();
        PlaySong(levelSong);
    }

    public void PlaySong(AudioClip clip)
    {
        audioS.clip = clip;
        audioS.Play();
    }
}
