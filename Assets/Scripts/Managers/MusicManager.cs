using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager singleton = null;

    public static MusicManager GetMusicManager() {
        return singleton;
    }
    void Awake() {
        if (singleton != null && singleton != this) {
            Destroy(gameObject);
            return;
        } else {
            singleton = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void ToggleMusic() {
        AudioSource music = gameObject.GetComponent<AudioSource>();
        if(music.isPlaying){
            music.Pause();
        } else {
            music.Play();
        }
    }

    public bool isPlaying() {
        AudioSource music = gameObject.GetComponent<AudioSource>();
        return music.isPlaying;
    }
}
