using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 「おとす」や「いれかえ」の処理
public class VaryPanel : MonoBehaviour {
    // 隙間の数
    int fillCount;

    /// パネルが何色か取得（SelectPanelクラスにもあるから、変更時注意）←本当は良くない
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

    /// パネルを移動させる
    void MovePanel(int column, int line, int newLine) {
        // 移動させるパネルの取得
        GameObject panel = GameObject.Find(column + " " + line);

        // パネルの位置の変更
        float newPositionX = panel.transform.position.x;
        float newPositionY = panel.transform.position.y - (this.GetComponent<GameDirector>().panelHeight * (line - newLine));
        float newPositionZ = panel.transform.position.z;
        panel.transform.position = new Vector3(newPositionX, newPositionY, newPositionZ);

        // パネルの名前の変更
        panel.name = column.ToString() + " " + newLine.ToString();

        // パネルの状態を管理する配列の更新
        this.GetComponent<GameDirector>().panelsId[column, newLine] = this.GetComponent<GameDirector>().panelsId[column, line];
        this.GetComponent<GameDirector>().panelsId[column, line] = 0;
    }

    /// 1枚のパネルを「おとす」
    void DropPanel(int column, int line) {
        // 真下にパネルがあれば落ちない
        if (GetPanelId(column, line) == 0) {
            return;
        }

        // 一番下のものは落ちない
        if (line <= 0) {
            return;
        }

        // パネルがないならパネルは落ちない
        if (GetPanelId(column, line - 1) != 0) {
            return;
        }

        // どこまで落ちるか
        int newLine = line;
        for (int i = line -1; i >= 0; i--) {
            if (GetPanelId(column, i) != 0) {
                break;
            }

            newLine = i;
        }

        MovePanel(column, line, newLine);
    }

    /// 「おとす」の落とす処理
    public void DropPanels() {
        for (int i = 0; i < this.GetComponent<GameDirector>().numOfColumns; i++) {
            for (int k = 0; k < this.GetComponent<GameDirector>().numOfLines; k++) {
                DropPanel(i, k);
            }
        }

        // 使えるボタンの更新
        GameObject button = GameObject.Find("ButtonCanvas");
        button.GetComponent<UIButton>().ChangeButtonColorAvailable(button.GetComponent<UIButton>().buttonReplace);
        button.GetComponent<UIButton>().ChangeButtonColorNotAvailable(button.GetComponent<UIButton>().buttonDrop);
        this.GetComponent<GameDirector>().hasSelected = false;
    }

    /// 隙間を埋める
    void FillPanel(int column, int line) {
        this.GetComponent<GameDirector>().SetPanel(column, line);

        fillCount++;
    }

    /// 「おとす」の落とした後の隙間を埋める処理
    public void FillPanels() {
        fillCount = 0;

        for (int i = 0; i < this.GetComponent<GameDirector>().numOfColumns; i++) {
            for (int k = 0; k < this.GetComponent<GameDirector>().numOfLines; k++) {
                if (this.GetComponent<GameDirector>().panelsId[i, k] == 0) {
                    FillPanel(i, k);
                }
            }
        }

        // スコア加算
        this.GetComponent<Score>().AddFillScore(fillCount, this.GetComponent<GameDirector>().phase);

        // SEを鳴らす
        this.GetComponent<SEController>().PlaySEDrop(fillCount);
    }
}
