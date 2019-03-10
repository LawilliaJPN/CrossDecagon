using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaryPanel :MonoBehaviour {
    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectOptionManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameOption scriptGameOption;

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectSEManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private SEManager scriptSEManager;

    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameDirector scriptGameDirector;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Score scriptScore;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectCanvasButton;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameButton scriptGameButton;

    private void Awake() {
        this.scriptGameDirector = this.GetComponent<GameDirector>();
        this.scriptScore = this.GetComponent<Score>();

        this.objectOptionManager = GameObject.FindWithTag("OptionManager");
        this.scriptGameOption = this.objectOptionManager.GetComponent<GameOption>();

        this.objectSEManager = GameObject.FindWithTag("SEManager");
        this.scriptSEManager = this.objectSEManager.GetComponent<SEManager>();

        this.scriptGameButton = this.objectCanvasButton.GetComponent<GameButton>();
    }

    private int GetPanelId(int column, int line) {
        if ((column < 0) || (column >= this.scriptGameDirector.NumOfColumns)) {
            return 0;
        }

        if ((line < 0) || (line >= this.scriptGameDirector.NumOfLines)) {
            return 0;
        }

        return this.scriptGameDirector.GetPanelsId(column, line);
    }

    private void MovePanel(int column, int line, int newLine) {
        GameObject panel = GameObject.Find(column + " " + line);

        float newPositionX = panel.transform.position.x;
        float newPositionY = panel.transform.position.y - (this.scriptGameDirector.PanelHeight * (line - newLine));
        float newPositionZ = panel.transform.position.z;
        panel.transform.position = new Vector3(newPositionX, newPositionY, newPositionZ);

        string debugOldName = panel.name;
        panel.name = column.ToString() + " " + newLine.ToString();

        this.scriptGameDirector.SetPanelsId(column, newLine, this.scriptGameDirector.GetPanelsId(column, line));
        this.scriptGameDirector.SetPanelsId(column, line, 0);


        // デバッグ用
        if (this.scriptGameOption.DebugMode) {
            string color = "";

            switch (this.scriptGameDirector.GetPanelsId(column, newLine)) {
                case 1:
                    color = "red";
                    break;
                case 2:
                    color = "yellow";
                    break;
                case 3:
                    color = "green";
                    break;
                
            }

            Debug.Log(panel.name + "(<=" + debugOldName + ")" + " is " + color);
        }
    }

    private void DropPanel(int column, int line) {
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
        for (int i = line - 1; i >= 0; i--) {
            if (GetPanelId(column, i) != 0) {
                break;
            }

            newLine = i;
        }

        MovePanel(column, line, newLine);
    }

    public void DropPanels() {
        for (int i = 0; i < this.scriptGameDirector.NumOfColumns; i++) {
            for (int k = 0; k < this.scriptGameDirector.NumOfLines; k++) {
                DropPanel(i, k);
            }
        }

        GameObject button = GameObject.Find("CanvasButton");
        this.scriptGameDirector.HasSelected = false;
    }

    private void FillPanel(int column, int line) {
        this.scriptGameDirector.SetPanel(column, line);

        this.scriptScore.FillCount++;
    }

    public void FillPanels() {
        this.scriptScore.FillCount = 0;

        for (int column = 0; column < this.scriptGameDirector.NumOfColumns; column++) {
            for (int line = 0; line < this.scriptGameDirector.NumOfLines; line++) {
                if (this.scriptGameDirector.GetPanelsId(column, line) == 0) {
                    FillPanel(column, line);
                }
            }
        }

        // スコア
        this.scriptScore.AddFillScore(this.scriptScore.FillCount, this.scriptGameDirector.Phase);

        // SE
        this.scriptSEManager.PlaySEDrop(this.scriptScore.FillCount);
    }
}
