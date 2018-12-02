using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// リザルト画面に表示するTips
public class Tips : MonoBehaviour {
    /// Tipsの抽選
    public string GetTips(){
        string tips ="";

        switch (Random.Range(0, 17+1)) {
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
                tips = "「ツイート」ボタンを押すと、スクショ付きでツイートできる";
                break;
            case 6:
                tips = "「ランキング」ボタンを押すと、ランキングを確認できる";
                break;
            case 7:
                tips = "「リトライ」ボタンを押すと、リトライすることができる";
                break;
            case 8:
                tips = "斜め十字(×型)に気付けるかどうかがハイスコアのカギだ";
                break;
            case 9:
                tips = "十字(＋型)と斜め十字(×型)を同時消しすると高スコアが狙える";
                break;
            case 10:
                tips = "一度にたくさん消すと高スコアが狙える";
                break;
            case 11:
                tips = "「おとす」をせずに何度も消すと高スコアが狙える";
                break;
            case 12:
                tips = "たくさん消してから「おとす」で高スコアが狙える";
                break;
            case 13:
                tips = "「おとす」をせずにフェイズが終わってもスコアは加算される";
                break;
            case 14:
                tips = "「いれかえ」を使いすぎると時間が無くなってしまう";
                break;
            case 15:
                tips = "消せるものが見つからない時は早めに「いれかえ」だ";
                break;
            case 16:
                tips = "消せるものが少ないときは「いれかえ」がオススメだ";
                break;
            case 17:
                tips = "お手つきすると、0.5秒間操作をできなくなる";
                break;
        }
        return tips;
    }
}
