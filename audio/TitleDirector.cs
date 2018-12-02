using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// タイトルとチュートリアルのGameDirectorのつもりが、音楽鳴らすだけになってしまった
public class TitleDirector : MonoBehaviour {
    AudioSource audioSource;

    public AudioClip bgmGameTitle;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgmGameTitle;
        audioSource.Play();
        audioSource.loop = true;
    }

    public void StopMusic() {
        audioSource.Stop();
    }
}