using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// SEを鳴らすためのクラス
public class SEController : MonoBehaviour {
    AudioSource audioSource;

    // ゲームを開始するボタンを押したときのSE
    public AudioClip seStart;

    // パネルをクリックしたときのSE
    public AudioClip seSelect;

    // 「おとす」をしたときのSE
    public AudioClip seDropA;
    public AudioClip seDropB;
    public AudioClip seDropC;

    // 残り時間が3、2、1秒になったときのSE
    public AudioClip seAlerm;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    /// ゲームを開始するボタンを押したとき
    public void PlaySEStart() {
        audioSource.PlayOneShot(seStart);
    }

    /// パネルをクリックしたとき
    public void PlaySESelect() {
        audioSource.PlayOneShot(seSelect);
    }

    /// 「おとす」をしたとき（count：消したパネルの個数）
    public void PlaySEDrop(int count) {
        if (count >= 20) {
            audioSource.PlayOneShot(seDropC);
        } else if (count >= 10) {
            audioSource.PlayOneShot(seDropB);
        } else if (count >= 5) {
            audioSource.PlayOneShot(seDropA);
        }
    }

    /// 残り時間が3、2、1秒になったとき
    void PlaySEAlerm() {
        audioSource.PlayOneShot(seAlerm);
    }

    /// 残り3秒になったとき
    public void PlaySEAlerms() {
        PlaySEAlerm();
        Invoke("PlaySEAlerm", 1.0f);
        Invoke("PlaySEAlerm", 2.0f);
    }
}
