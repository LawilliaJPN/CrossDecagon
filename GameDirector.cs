using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

/// ゲーム画面・リザルト画面の主な処理のためのクラス
public class GameDirector : SerializedMonoBehaviour {
    // 定数
    public int numOfColumns = 8;
    public int numOfLines = 8;
    public int numOfPanelType = 2;

    // ゲームの状態
    public int[,] panelsId = new int[8, 8];
    public int phase;
    public bool hasSelected;

    // タイマー
    public GameObject timer;
    public GameObject timerImage;
    public float time;
    bool hasTimeSE;

    // お手つき
    public GameObject penaltyPanel;
    public float timerPenalty;

    // 各パネルのサイズ
    public float panelWidth;
    public float panelHeight;

    void Start() {
        panelWidth = 1.0f;
        panelHeight = 1.0f;

        phase = 1;
        hasSelected = false;

        time = 10;
        hasTimeSE = false;
        timerPenalty = 0;

        // BGMを鳴らす
        GameObject mainCamera = GameObject.Find("MainCamera");
        mainCamera.GetComponent<AudioController>().PlayGameMusic();

        SetPanels();
    }

    void Update() {
        // ゲーム中
        if (phase <= 10) {
            // タイマーを動かす
            time -= Time.deltaTime;
            timerPenalty -= Time.deltaTime;
            timer.GetComponent<Text>().text = System.Math.Ceiling(time).ToString();
            timerImage.GetComponent<Image>().fillAmount = time / 10;

            // SEを鳴らす
            if ((time <= 3) && (!hasTimeSE)) {
                this.GetComponent<SEController>().PlaySEAlerms();
                hasTimeSE = true;
            }
        }

        // お手つき時のパネル隠し
        if (timerPenalty > 0) {
            penaltyPanel.SetActive(true);
        } else {
            penaltyPanel.SetActive(false);
        }

        // フェーズ終了
        if (time <= 0) {
            TimeZero();
        }
    }

    /// フェーズ終了時に呼ばれるメソッド
    void TimeZero() {
        if (phase >= 10) {
            GameFinish();
        } else {
            NextPhase();
        }
    }

    /// 次のフェーズに行くときに呼ばれるメソッド
    void NextPhase() {
        // 「おとす」（「おとす」前にフェーズが切り替わってしまった分のスコアを反映させるため）
        this.GetComponent<VaryPanel>().DropPanels();
        this.GetComponent<VaryPanel>().FillPanels();

        // 「いれかえ」
        ResetPanels();

        // 時間関連のリセット
        time = 10;
        timer.GetComponent<Text>().text = System.Math.Ceiling(time).ToString();
        timerImage.GetComponent<Image>().fillAmount = time / 10;
        hasTimeSE = false;
        timerPenalty = 0;

        // スコア関連のリセット
        this.GetComponent<Score>().selectCount = 0;

        // ゲーム後半でBGM変更
        if (phase == 5) {
            GameObject mainCamera = GameObject.Find("MainCamera");
            mainCamera.GetComponent<AudioController>().SwitchGameMusic();
        }

        phase++;
    }

    /// 10フェーズ目が終わったときに呼ばれるメソッド
    void GameFinish() {
        // BGM変更
        GameObject mainCamera = GameObject.Find("MainCamera");
        mainCamera.GetComponent<AudioController>().PlayResultMusic();

        // 「おとす」（「おとす」前にゲームが終わってしまった分のスコアを反映させるため）
        this.GetComponent<VaryPanel>().DropPanels();
        this.GetComponent<VaryPanel>().FillPanels();

        // パネルの非表示
        DestoryPanels();

        // 表示するボタンの変更（メソッド名おかしいけど許して）
        GameObject button = GameObject.Find("ButtonCanvas");
        button.GetComponent<UIButton>().HideButtons();

        // 評価を表示
        this.GetComponent<Score>().OutputRank();

        // 時間関連のリセット
        time = 10;
        timer.GetComponent<Text>().text = "";
        timerImage.GetComponent<Image>().fillAmount = 0;
        timerPenalty = 0;

        // スコア関連のリセット
        this.GetComponent<Score>().selectCount = 0;

        phase++;

        // オンラインランキング
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(this.GetComponent<Score>().GetTotalScore());
    }

    /// 「いれかえ」
    public void ResetPanels() {
        DestoryPanels();
        SetPanels();
    }

    /// パネル全削除
    void DestoryPanels() {
        GameObject panels = GameObject.Find("Panels");

        foreach (Transform panel in panels.transform) {
            Destroy(panel.gameObject);
        } 
    }

    /// パネル1枚の設置
    public void SetPanel(int column, int line) {
        // 新規パネルの抽選
        panelsId[column, line] = Random.Range(1, numOfPanelType + 1);

        // 新規パネルの位置
        float positionX = (panelWidth * column) - 8.2f;
        float positionY = (panelHeight * line) - 4.2f;

        // 新規パネルの描画
        GetComponent<PanelGenerator>().InstantiatePanel(panelsId[column, line], column, line, positionX, positionY);
    }

    /// 全てのパネルを設置
    void SetPanels() {
        for (int i = 0; i < numOfColumns; i++) {
            for (int k = 0; k < numOfLines; k++) {
                SetPanel(i, k);
            }
        }
    }
}
