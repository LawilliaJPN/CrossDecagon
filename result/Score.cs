using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// スコアの管理
public class Score : MonoBehaviour {
    GameObject director;

    // スコア周りのUIオブジェクト
    public GameObject uiPhaseScores;
    public GameObject uiTotalScore;
    public GameObject uiTextRank;
    public GameObject uiTextNextRank;
    public GameObject uiTextTips;
    public GameObject uiTextTipsTitle;

    // フェーズごとのスコア
    int[] phaseScores = new int[10];

    // スコア加算のためのカウンタ
    public int destroyCount;
    public int selectCount;

    // 評価
    public string rank;
    public string nextRank;

    void Start () {
        director = GameObject.Find("GameDirector");

        for (int i = 0; i < phaseScores.Length; i++) {
            phaseScores[i] = 0;
        }
    }

    /// 合計スコアの計算・取得
    public int GetTotalScore() {
        int score = 0;
        for (int i = 0; i < phaseScores.Length; i++) {
            score += phaseScores[i];
        }
        return score;
    }

    /// 合計スコアの表示（何故ここでも計算しているのか分からない）
    private void OutputTotalScore () {
        int score = 0;
        for (int i = 0; i < phaseScores.Length; i++) {
            score += phaseScores[i];
        }
        string text = score.ToString("D9");

        uiTotalScore.GetComponent<Text>().text = text;
    }

    /// フェーズごとのスコアの表示
    private void OutputPhaseScore (GameObject uiObject, int phase) {
        string text = phase + ":" + phaseScores[phase-1].ToString("D8");
        uiObject.GetComponent<Text>().text = text;
    }

    /// スコアの表示の更新
    public void OutputScores() {
        foreach (Transform child in uiPhaseScores.transform) {
            OutputPhaseScore(child.gameObject, int.Parse(child.name));
        }

        OutputTotalScore();
    }

    /// 「おとす」をした時に隙間の数だけ加算される
    public void AddFillScore(int count, int phase) {
        if (count <= 0) {
            return;
        }

        if (phase > 10) {
            return;
        }

        int score = 0;
        for (int i = count; i > 0; i--) {
            score += i * i * i * 10;
        }

        phaseScores[phase - 1] += score;

        OutputScores();
    }

    /// パネルをクリックした時に消したパネルの数だけ加算される
    public void AddDestroyScore(int count, int phase) {
        if (count <= 0) {
            return;
        }

        if (phase > 10) {
            return;
        }

        int score = 0;
        for (int i = count; i > 0; i--) {
            score += i * i * i * i;
        }

        phaseScores[phase - 1] += score;

        OutputScores();
    }

    /// 1度の「おとす」でパネルをクリックした回数に応じて加算される（クリックするごとに）
    public void AddSelectScore(int count, int phase) {
        if (count <= 0) {
            return;
        }

        if (phase > 10) {
            return;
        }

        phaseScores[phase - 1] += 10000 * count * count;

        OutputScores();
    }

    /// 縦横十字と斜め十字を同時に消したときに加算される
    public void AddBonusScore (int phase) {
        if (phase > 10) {
            return;
        }

        phaseScores[phase - 1] += 100000; // スコア設定

        OutputScores();
    }

    /// 評価の表示
    public void OutputRank() {
        UpdateRank();

        uiTextRank.GetComponent<Text>().text = rank;
        uiTextNextRank.GetComponent<Text>().text = nextRank;

        uiTextTips.GetComponent<Text>().text = director.GetComponent<Tips>().GetTips();
        uiTextTipsTitle.GetComponent<Text>().text = "Tips";
    }

    /// 次の評価になるためのスコアの表示
    void UpdateRank() {
        int score = GetTotalScore();

        if (score >= 50000000) {
            rank = "評価：SSSランク";
            nextRank = "最高ランクだ！おめでとう！";
        } else if (score >= 10000000) {
            rank = "評価：SSランク";
            nextRank = "次のランク：500,000,000以上";
        } else if (score >= 6000000) {
            rank = "評価：Sランク";
            nextRank = "次のランク：10,000,000以上";
        } else if (score >= 3000000) {
            rank = "評価：Aランク";
            nextRank = "次のランク：6,000,000以上";
        } else if (score >= 1000000) {
            rank = "評価：Bランク";
            nextRank = "次のランク：3,000,000以上";
        } else if (score >= 100000) {
            rank = "評価：Cランク";
            nextRank = "次のランク：1,000,000以上";
        } else if (score > 0) {
            rank = "評価：Dランク";
            nextRank = "次のランク：100,000以上";
        } else {
            rank = "評価：Eランク";
            nextRank = "次のランク：1以上";
        }
    }

}
