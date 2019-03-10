using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Tips : MonoBehaviour {
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Score scriptScore;

    private void Awake() {
        this.scriptScore = this.GetComponent<Score>();
    }

    public string GetTips() {
        string tips ="";
        int score = this.scriptScore.GetTotalScore();

        if (score < 1000000) {
            // Cランク以下
            switch (Random.Range(0, 10 + 1)) {
                // 変更時は乱数の範囲に注意！！

                case 0:
                    tips = "「Dキー」でもパネルを「おとす」ができる";
                    break;
                case 1:
                    tips = "「Rキー」でもパネルを「いれかえ」ができる";
                    break;
                case 2:
                    tips = "「スペースキー」でもパネルを「いれかえ」ができる";
                    break;
                case 3:
                    tips = "パネルに隙間があるときは「いれかえ」ができない";
                    break;
                case 4:
                    tips = "パネルに隙間がないときは「おとす」ができない";
                    break;
                case 5:
                    tips = "「TWEET」ボタンを押すと、スクショ付きでツイートできる";
                    break;
                case 6:
                    tips = "「RANKING」ボタンを押すと、ランキングを確認できる";
                    break;
                case 7:
                    tips = "「RETRY」ボタンを押すと、リトライすることができる";
                    break;
                case 8:
                    tips = "消せるものが見つからない時は早めに「いれかえ」だ";
                    break;
                case 9:
                    tips = "消せるものが少ないときは「いれかえ」がオススメだ";
                    break;
                case 10:
                    tips = "お手つきすると、0.5秒間操作をできなくなる";
                    break;
            }


        } else if (score < 6000000) {
            // Bランク・Aランク
            switch (Random.Range(0, 8 + 1)) {
                // 変更時は乱数の範囲に注意！！

                case 0:
                    tips = "斜め十字(×型)に気付けるかどうかがハイスコアのカギだ";
                    break;
                case 1:
                    tips = "十字(＋型)と斜め十字(×型)を同時消しするとボーナスだ";
                    break;
                case 2:
                    tips = "一度にたくさん消すと高スコアが狙える";
                    break;
                case 3:
                    tips = "「おとす」をせずに何度も消すと高スコアが狙える";
                    break;
                case 4:
                    tips = "たくさん消してから「おとす」で高スコアが狙える";
                    break;
                case 5:
                    tips = "「おとす」をせずにフェイズが終わってもスコアは加算される";
                    break;
                case 6:
                    tips = "のんびり「いれかえ」をしていると時間が無くなってしまう";
                    break;
                case 7:
                    tips = "「いれかえ」を早めに決断することが重要だ";
                    break;
                case 8:
                    tips = "お手つきすると、0.5秒間操作をできなくなる";
                    break;
            }

        } else if (score < 20000000) {
            // Sランク・SSランク
            switch (Random.Range(0, 14 + 1)) {
                // 変更時は乱数の範囲に注意！！

                case 0:
                    tips = "ボーナススコア：1回につき100,000加算";
                    break;
                case 1:
                    tips = "デストロイスコア：同時に消したパネルの枚数に応じて加算";
                    break;
                case 2:
                    tips = "ボーナススコア：十字・斜め十字を同時消しで加算";
                    break;
                case 3:
                    tips = "フィルスコア：同時に埋めたパネルの枚数に応じて加算";
                    break;
                case 4:
                    tips = "セレクトスコア：「おとす」までに消した回数に応じて加算";
                    break;
                case 5:
                    tips = "スコア加算は「フィル」「デストロイ」「セレクト」「ボーナス」の4種";
                    break;
                case 6:
                    tips = "フィルスコア：「おとす」時やフェーズの終わりに加算";
                    break;
                case 7:
                    tips = "セレクトスコア：パネルを消したときに加算";
                    break;
                case 8:
                    tips = "デストロイスコア：パネルを消したときに加算";
                    break;
                case 9:
                    tips = "セレクトスコア：「おとす」までに4回以上パネルを消したい";
                    break;
                case 10:
                    tips = "フィルスコア：20個以上まとめて「おとす」をしたい";
                    break;
                case 11:
                    tips = "デストロイスコア：一度に10個以上のパネルを消したい";
                    break;
                case 12:
                    tips = "2,000,000以上取るようなフェーズを増やすことが重要だ";
                    break;
                case 13:
                    tips = "500,000以上取れないようなフェーズを減らすことが重要だ";
                    break;
                case 14:
                    tips = "20個以上まとめて「おとす」とSEが豪華になる";
                    break;
            }

        } else {
            // SSSランク
            tips = "開発者の想定を超えたスコアだ";
        }

        return tips;
    }
}
