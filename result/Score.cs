using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class Score : MonoBehaviour {
    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectOptionManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameOption scriptGameOption;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectPhaseScores, objectTotalScore, objectTextRank, objectTextNextRank, objectTextTips, objectTextTipsTitle;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text textTotalScore, textRank, textNextRank, textTips, textTipsTitle;

    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Tips scriptTips;

    [BoxGroup("Score"), ShowInInspector, ReadOnly]
    private int[] phaseScores = new int[10];

    [BoxGroup("Counter"), ShowInInspector, ReadOnly]
    private int destroyCount, selectCount, fillCount;

    public int DestroyCount {
        get {
            return destroyCount;
        }
        set {
            destroyCount = value;
        }
    }

    public int SelectCount {
        get {
            return selectCount;
        }
        set {
            selectCount = value;
        }
    }

    public int FillCount {
        get {
            return fillCount;
        }
        set {
            fillCount = value;
        }
    }

    [BoxGroup("Rank"), ShowInInspector, ReadOnly]
    private string rank, nextRank;

    public string Rank {
        get {
            return rank;
        }
    }

    public string NextRank {
        get {
            return nextRank;
        }
    }

    private void Awake() {
        this.objectOptionManager = GameObject.FindWithTag("OptionManager");
        this.scriptGameOption = this.objectOptionManager.GetComponent<GameOption>();

        this.textTotalScore = this.objectTotalScore.GetComponent<Text>();
        this.textRank = this.objectTextRank.GetComponent<Text>();
        this.textNextRank = this.objectTextNextRank.GetComponent<Text>();
        this.textTips = this.objectTextTips.GetComponent<Text>();
        this.textTipsTitle = this.objectTextTipsTitle.GetComponent<Text>();

        this.scriptTips = this.GetComponent<Tips>();

        for (int i = 0; i < this.phaseScores.Length; i++) {
            this.phaseScores[i] = 0;
        }
    }

	void Update () {
        // デバッグ用
        if (this.scriptGameOption.DebugMode) {
            if (Input.GetKeyDown(KeyCode.S)) {
                OutputScores();
            }
        }
    }

    public void OutputRank() {
        UpdateRank();
        this.textRank.text = rank;
        this.textNextRank.text = nextRank;

        this.textTips.text = this.scriptTips.GetTips();
        this.textTipsTitle.text = "Tips";
    }

    void UpdateRank() {
        int score = GetTotalScore();

        if (score >= 20000000) {
            rank = "評価：SSSランク";
            nextRank = "最高ランクだ！おめでとう！";

        } else if (score >= 10000000) {
            rank = "評価：SSランク";
            nextRank = "次のランク：20,000,000以上";

        } else if (score >= 6000000) {
            rank = "評価：Sランク";
            nextRank = "次のランク：10,000,000以上";

        } else if (score >= 3000000) {
            rank = "評価：Aランク";
            nextRank = "次のランク：6,000,000以上";

        } else if (score >= 1000000) {
            rank = "評価：Bランク";
            nextRank = "次のランク：3,000,000以上";

        } else if (score >= 500000) {
            rank = "評価：Cランク";
            nextRank = "次のランク：1,000,000以上";

        } else if (score > 100000) {
            rank = "評価：Dランク";
            nextRank = "次のランク：500,000以上";

        } else {
            rank = "評価：Eランク";
            nextRank = "次のランク：100,000以上";
        }
    }

    public int GetTotalScore() {
        int score = 0;
        for (int i = 0; i < this.phaseScores.Length; i++)
        {
            score += this.phaseScores[i];
        }
        return score;
    }

    private void OutputTotalScore () {
        string text = GetTotalScore().ToString("D9");

        this.textTotalScore.text = text;
    }

    private void OutputPhaseScore (GameObject objectPhaseScore, int phase) {
        string text = phase + ":" + this.phaseScores[phase-1].ToString("D8");
        objectPhaseScore.GetComponent<Text>().text = text;
    }

    public void OutputScores() {
        foreach (Transform transformPhaseScore in this.objectPhaseScores.transform) {
            OutputPhaseScore(transformPhaseScore.gameObject, int.Parse(transformPhaseScore.name));
        }

        OutputTotalScore();

        // デバッグ用
        if (this.scriptGameOption.DebugMode) {
            for (int i = 0; i < phaseScores.Length; i++) {
                Debug.Log(i.ToString("D2") + ":" + phaseScores[i]);
            }
        }
    }

    // 「おとす」をした時に、埋めたパネルの枚数に応じて加算されるスコア
    public void AddFillScore(int count, int phase) {
        if (count <= 0) {
            return;
        }

        if (phase > 10) {
            return;
        }

        int score = 0;
        for (int i = count; i > 0; i--) {
            // スコア設定
            score += i * i * i * 10;
        }

        phaseScores[phase - 1] += score;

        OutputScores();
    }

    // パネルを選択した時に、一度に消した枚数に応じて加算されるスコア
    public void AddDestroyScore(int count, int phase) {
        if (count <= 0) {
            return;
        }

        if (phase > 10) {
            return;
        }

        int score = 0;
        for (int i = count; i > 0; i--) {
            // スコア設定
            score += i * i * i * i;
        }

        phaseScores[phase - 1] += score;

        OutputScores();
    }

    // パネルを選択した時に、「おとす」までに消した回数に応じて加算されるスコア
    public void AddSelectScore(int count, int phase) {
        if (count <= 0) {
            return;
        }

        if (phase > 10) {
            return;
        }

        // スコア設定
        phaseScores[phase - 1] += 10000 * count * count;

        OutputScores();
    }

    // 十字・斜め十字を同時消しした時に加算されるスコア
    public void AddBonusScore (int phase) {
        if (phase > 10) {
            return;
        }

        // スコア設定
        phaseScores[phase - 1] += 100000;

        OutputScores();
    }

}
