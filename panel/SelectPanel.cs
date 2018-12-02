using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// パネルをクリックした時のためのクラス
public class SelectPanel : MonoBehaviour {
    GameObject director;

    void Start () {
        director = GameObject.Find("GameDirector");
    }

    // パネルをクリックしたときに呼ばれる
    private void OnMouseDown() {
        // 「お手つき」
        if (director.GetComponent<GameDirector>().timerPenalty > 0) {
            return;
        }

        // パネルの名前からパネルの位置を取得
        string[] box = name.Split(' ');
        int column = int.Parse(box[0]);
        int line = int.Parse(box[1]);

        director.GetComponent<Score>().destroyCount = 0;

        // 十字と斜め十字を同時に消したらボーナススコア
        if ((IsCenterOfCross(column, line)) && (IsCenterOfSaltire(column, line))) {
            director.GetComponent<Score>().AddBonusScore(director.GetComponent<GameDirector>().phase);
        }

        if ((IsCenterOfCross(column, line)) || (IsCenterOfSaltire(column, line))) {
            // SEを鳴らす
            director.GetComponent<SEController>().PlaySESelect();

            // 「おとす」「いれかえ」ボタンの状態変更
            GameObject button = GameObject.Find("ButtonCanvas");
            button.GetComponent<UIButton>().ChangeButtonColorNotAvailable(button.GetComponent<UIButton>().buttonReplace);
            button.GetComponent<UIButton>().ChangeButtonColorAvailable(button.GetComponent<UIButton>().buttonDrop);
            director.GetComponent<GameDirector>().hasSelected = true;

            // 縦横十字を消す
            if (IsCenterOfCross(column, line)) {
                DestroyCross(column, line);
            }

            // 斜め十字を消す
            if (IsCenterOfSaltire(column, line)) {
                DestroySaltire(column, line);
            }

            // 自身を消す
            DestroyPanel(column, line);

            // スコア加算
            director.GetComponent<Score>().selectCount++;
            director.GetComponent<Score>().AddSelectScore(director.GetComponent<Score>().selectCount, director.GetComponent<GameDirector>().phase);
            director.GetComponent<Score>().AddDestroyScore(director.GetComponent<Score>().destroyCount, director.GetComponent<GameDirector>().phase);

        } else {
            // 「お手つき」
            director.GetComponent<GameDirector>().timerPenalty = 0.5f;
        }
    }

    /// パネルが何色か取得（VaryPanelクラスにもあるから、変更時注意）←本当は良くない
    int GetPanelId(int column, int line) {
        // 範囲外のパネル
        if ((column < 0) || (column >= director.GetComponent<GameDirector>().numOfColumns)) {
            return 0;
        }
        if ((line < 0) || (line >= director.GetComponent<GameDirector>().numOfLines)) {
            return 0;
        }

        return director.GetComponent<GameDirector>().panelsId[column, line];
    }

    /// パネル1枚を壊す
    void DestroyPanel(int column, int line) {
        GameObject panel = GameObject.Find(column + " " + line);
        Destroy(panel);

        director.GetComponent<GameDirector>().panelsId[column, line] = 0;

        director.GetComponent<Score>().destroyCount++;
    }

    /// 十字方向のパネルを壊す
    void DestroyCross(int column, int line) {
        // 無限ループもどき
        int max = 100000;

        // 四方向に順番に
        for (int i = 1; i < max; i++) {
            if (GetPanelId(column, line) == GetPanelId(column + i, line)) {
                DestroyPanel(column + i, line);
            } else {
                break;
            }
        }

        for (int i = 1; i < max; i++) {
            if (GetPanelId(column, line) == GetPanelId(column - i, line)) {
                DestroyPanel(column - i, line);
            } else {
                break;
            }
        }

        for (int i = 1; i < max; i++) {
            if (GetPanelId(column, line) == GetPanelId(column, line + i)) {
                DestroyPanel(column, line + i);
            } else {
                break;
            }
        }

        for (int i = 1; i < max; i++) {
            if (GetPanelId(column, line) == GetPanelId(column, line - i)) {
                DestroyPanel(column, line - i);
            } else {
                break;
            }
        }
    }

    /// 斜め十字方向のパネルを壊す
    void DestroySaltire(int column, int line) {
        // 無限ループもどき
        int max = 100000;

        // 四方向に順番に
        for (int i = 1; i < max; i++) {
            if (GetPanelId(column, line) == GetPanelId(column + i, line + i)) {
                DestroyPanel(column + i, line + i);
            } else {
                break;
            }
        }

        for (int i = 1; i < max; i++) {
            if (GetPanelId(column, line) == GetPanelId(column + i, line - i)) {
                DestroyPanel(column + i, line - i);
            } else {
                break;
            }
        }

        for (int i = 1; i < max; i++) {
            if (GetPanelId(column, line) == GetPanelId(column - i, line + i)) {
                DestroyPanel(column - i, line + i);
            } else {
                break;
            }
        }

        for (int i = 1; i < max; i++) {
            if (GetPanelId(column, line) == GetPanelId(column - i, line - i)) {
                DestroyPanel(column - i, line - i);
            } else {
                break;
            }
        }
    }

    // 十字方向に消せるか判定
    bool IsCenterOfCross(int column, int line) {
        int selectId = GetPanelId(column, line);

        // パネルが何もないところをクリックしたとき
        if (selectId == 0) {
            return false;
        }

        if ((selectId != GetPanelId(column, line - 1))
            || (selectId != GetPanelId(column, line + 1))
            || (selectId != GetPanelId(column - 1, line))
            || (selectId != GetPanelId(column + 1, line))) {
            return false;
        }

        return true;
    }

    // 斜め十字方向に消せるか判定
    bool IsCenterOfSaltire(int column, int line) {
        int selectId = GetPanelId(column, line);

        // パネルが何もないところをクリックしたとき
        if (selectId == 0) {
            return false;
        }

        if ((selectId != GetPanelId(column - 1, line - 1))
            || (selectId != GetPanelId(column - 1, line +1))
            || (selectId != GetPanelId(column + 1, line -1))
            || (selectId != GetPanelId(column + 1, line +1))) {
            return false;
        }

        return true;
    }
}
