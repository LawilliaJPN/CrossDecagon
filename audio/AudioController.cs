using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// ゲーム画面とリザルト画面のBGM関連
public class AudioController : MonoBehaviour {
    AudioSource audioSource;

    // リザルト画面のBGM
    public AudioClip bgmResult;

    // ゲーム前半のBGM（抽選）
    public AudioClip bgmGameFirstA;
    public AudioClip bgmGameFirstB;

    // ゲーム後半のBGM
    public AudioClip bgmGameLatter;

    void Start () {
        audioSource = GetComponent<AudioSource>();
    }

    /// BGMを止めるだけ（いらなかった）
    public void StopMusic() {
        audioSource.Stop();
    }

    /// ゲーム前半のBGMを鳴らす
    public void PlayGameMusic() {
        switch (Random.Range(0, 1 + 1)) {
            case 0:
                audioSource.clip = bgmGameFirstA;
                break;
            case 1:
                audioSource.clip = bgmGameFirstB;
                break;
        }
        audioSource.Play();
    }

    /// ゲーム後半のBGMを鳴らす
    public void SwitchGameMusic() {
        audioSource.Stop();
        audioSource.clip = bgmGameLatter;
        audioSource.Play();
    }

    /// リザルト画面のBGMを鳴らす
    public void PlayResultMusic() {
        audioSource.Stop();
        audioSource.clip = bgmResult;
        audioSource.Play();
    }
}