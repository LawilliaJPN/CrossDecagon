using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGenerator : MonoBehaviour {
    // パネルのプレハブ
    public GameObject prefabPanelA;
    public GameObject prefabPanelB;
    public GameObject prefabPanelC;

    /// パネルの生成
    public void InstantiatePanel(int id, int column, int line, float positionX, float positionY) {
        GameObject panels = GameObject.Find("Panels");
        GameObject panel = null;

        switch (id) {
            case 1:
                panel = Instantiate(prefabPanelA) as GameObject;
                break;
            case 2:
                panel = Instantiate(prefabPanelB) as GameObject;
                break;
            case 3:
                panel = Instantiate(prefabPanelC) as GameObject;
                break;
        }

        // パネルの位置
        panel.transform.position = new Vector3(positionX, positionY, 0);

        // パネルの名前（名前からパネルの位置を取得するため）
        panel.name = column.ToString() + " " + line.ToString();

        // オブジェクトをまとめる（まとめて処理したい時のため）
        panel.transform.SetParent(panels.transform);
    }
}
