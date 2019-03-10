using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPanel :MonoBehaviour {
    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectOptionManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameOption scriptGameOption;

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectSEManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private SEManager scriptSEManager;

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectGameDirector;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameDirector scriptGameDirector;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Score scriptScore;

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectCanvasButton;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameButton scriptGameButton;

    private void Awake() {
        this.objectGameDirector = GameObject.FindWithTag("GameDirector");
        this.scriptGameDirector = this.objectGameDirector.GetComponent<GameDirector>();
        this.scriptScore = this.objectGameDirector.GetComponent<Score>();

        this.objectOptionManager = GameObject.FindWithTag("OptionManager");
        this.scriptGameOption = this.objectOptionManager.GetComponent<GameOption>();

        this.objectSEManager = GameObject.FindWithTag("SEManager");
        this.scriptSEManager = this.objectSEManager.GetComponent<SEManager>();

        this.objectCanvasButton = GameObject.FindWithTag("CanvasButton");
        this.scriptGameButton = this.objectCanvasButton.GetComponent<GameButton>();
    }

    private void OnMouseDown() {
        if (this.scriptGameDirector.TimerPenalty > 0) {
            return;
        }

        string[] box = name.Split(' ');
        int column = int.Parse(box[0]);
        int line = int.Parse(box[1]);

        this.scriptScore.DestroyCount = 0;

        if ((IsCenterOfCross(column, line)) && (IsCenterOfSaltire(column, line))) {
            this.scriptScore.AddBonusScore(this.scriptGameDirector.Phase);
        }

        if ((IsCenterOfCross(column, line)) || (IsCenterOfSaltire(column, line))) {
            this.scriptSEManager.PlaySESelect();

            this.scriptGameDirector.HasSelected = true;

            if (IsCenterOfCross(column, line)) {
                DestroyCross(column, line);
            }

            if (IsCenterOfSaltire(column, line)) {
                DestroySaltire(column, line);
            }

            DestroyPanel(column, line);

            this.scriptScore.SelectCount++;
            this.scriptScore.AddSelectScore(this.scriptScore.SelectCount, this.scriptGameDirector.Phase);
            this.scriptScore.AddDestroyScore(this.scriptScore.DestroyCount, this.scriptGameDirector.Phase);
        } else {
            if (this.scriptGameDirector.Timer < 9.5f) {
                this.scriptGameDirector.TimerPenalty = 0.5f;
            }
        }

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

    private void DestroyPanel(int column, int line) {
        GameObject panel = GameObject.Find(column + " " + line);
        Destroy(panel);

        this.scriptGameDirector.SetPanelsId(column, line, 0);

        this.scriptScore.DestroyCount++;
    }

    private void DestroyCross(int column, int line) {
        int max = 100000;

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


    private void DestroySaltire(int column, int line) {
        int max = 100000;

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

    private bool IsCenterOfCross(int column, int line) {
        int selectId = GetPanelId(column, line);

        if (selectId == 0) {
            if (this.scriptGameOption.DebugMode) {
                Debug.Log("IsCenterOfCross selectId：0");
            }
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

    private bool IsCenterOfSaltire(int column, int line) {
        int selectId = GetPanelId(column, line);

        if (selectId == 0) {
            if (this.scriptGameOption.DebugMode) {
                Debug.Log("IsCenterOfSaltire selectId：0");
            }
            return false;
        }

        if ((selectId != GetPanelId(column - 1, line - 1))
            || (selectId != GetPanelId(column - 1, line + 1))
            || (selectId != GetPanelId(column + 1, line - 1))
            || (selectId != GetPanelId(column + 1, line + 1))) {
            return false;
        }

        return true;
    }
}
