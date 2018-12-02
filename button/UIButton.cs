using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// ゲーム画面、リザルト画面のボタン関連
public class UIButton : MonoBehaviour {
    GameObject director;

    // ゲーム画面のボタン
    public GameObject buttonDrop;
    public GameObject buttonReplace;

    // リザルト画面のボタン
    public GameObject buttonTweet;
    public GameObject buttonRanking;
    public GameObject buttonRetry;

    void Start () {
        director = GameObject.Find("GameDirector");

        // ゲーム画面のボタンは初期表示
        buttonDrop.SetActive(true);
        buttonReplace.SetActive(true);

        // リザルト画面のボタンは初期非表示
        buttonTweet.SetActive(false);
        buttonRanking.SetActive(false);
        buttonRetry.SetActive(false);
    }
	
    void Update () {
        // 「おとす」
        if (Input.GetKeyDown(KeyCode.D)) {
            ButtonDrop();
        }
        
        // 「いれかえ」
        if (Input.GetKeyDown(KeyCode.R)) {
            ButtonReplace();
        }

        // 「いれかえ」
        if (Input.GetKeyDown(KeyCode.Space)) {
            ButtonReplace();
        }
    }

    /// 「おとす」ボタン
    public void ButtonDrop() {
        // リザルト画面
        if (director.GetComponent<GameDirector>().phase > 10) {
            return;
        }

        // 隙間がない時
        if (!(director.GetComponent<GameDirector>().hasSelected))
        {
            return;
        }

        // 「お手つき」中
        if (director.GetComponent<GameDirector>().timerPenalty > 0) {
            return;
        }

        director.GetComponent<VaryPanel>().DropPanels();
        director.GetComponent<VaryPanel>().FillPanels();
    }

    /// 「いれかえ」ボタン
    public void ButtonReplace() {
        // リザルト画面
        if (director.GetComponent<GameDirector>().phase > 10) {
            return;
        }

        // 隙間がある時
        if (director.GetComponent<GameDirector>().hasSelected) {
            return;
        }

        // 「お手つき」中
        if (director.GetComponent<GameDirector>().timerPenalty > 0) {
            return;
        }

        director.GetComponent<GameDirector>().ResetPanels();
    }

    /// 「リトライ」ボタン
    public void ButtonRetry() {
        GameObject mainCamera = GameObject.Find("MainCamera");
        mainCamera.GetComponent<AudioController>().StopMusic();

        // 「リトライ」が完了するまで操作できないようにするため
        director.GetComponent<GameDirector>().phase = 11;

        director.GetComponent<SEController>().PlaySEStart();
        Invoke("Retry", 1.0f);
    }

    void Retry() {
        SceneManager.LoadScene("Game");
    }

    /// 「ランキング」ボタン
    public void ButtonRanking() {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(director.GetComponent<Score>().GetTotalScore());
    }

    /// 「ツイート」ボタン
    public void ButtonTweet() {
        int score = director.GetComponent<Score>().GetTotalScore();
        string tweetSentence = "【クロスデカゴン ver.1.1】今回のスコア：" + score + "(" + director.GetComponent<Score>().rank + "）";
        naichilab.UnityRoomTweet.TweetWithImage("cross_decagon", tweetSentence, "unity1week", "cross_decagon");
    }

    /// 使用できるボタンを不透明に
    public void ChangeButtonColorAvailable(GameObject button) {
        button.GetComponent<Image>().color = new Color32(250, 170, 32, 255);
    }

    /// 使用できないボタンを半透明に
    public void ChangeButtonColorNotAvailable(GameObject button) {
        button.GetComponent<Image>().color = new Color32(250, 170, 32, 64);
    }

    /// ゲーム終了時に表示するボタンを切り替える処理（メソッド名が分かりづらいけど許して）
    public void HideButtons() {
        buttonDrop.SetActive(false);
        buttonReplace.SetActive(false);

        buttonTweet.SetActive(true);
        buttonRanking.SetActive(true);
        buttonRetry.SetActive(true);
    }
}
