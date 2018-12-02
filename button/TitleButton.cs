using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour {
    AudioSource audioSource;

    // ゲームを開始するボタンを押したときのSE
    public AudioClip seStart;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    /// 「スタート」ボタン
    public void ButtonStart() {
        GameObject director = GameObject.Find("GameDirector");
        director.GetComponent<TitleDirector>().StopMusic();

        audioSource.PlayOneShot(seStart);
        Invoke("GameStart", 1.0f);
    }

    void GameStart() {
        SceneManager.LoadScene("Game");
    }

    /// 「遊び方」ボタン
    public void ButtonTutorial() {
        SceneManager.LoadScene("Tutorial");
    }

    /// 「ランキング」ボタン
    public void ButtonRanking() {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(0);
    }
}
