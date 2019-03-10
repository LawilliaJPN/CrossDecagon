using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class GameDirector : SerializedMonoBehaviour {

    // スクリプト

    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Score scriptScore;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private VaryPanel scriptVaryPanel;

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectOptionManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameOption scriptGameOption;

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectBGMManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private BGMManager scriptBGMManager;

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectSEManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private SEManager scriptSEManager;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectCanvasButton;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameButton scriptGameButton;

    // プレハブ

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectPanels;
    [SerializeField, BoxGroup("Prefab")]
    private GameObject prefabPanel;

    // タイマー

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectTextTimer, objectImageTimer, objectImageTimerBack, objectPenaltyPanel;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text textTimer;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageTimer, imageTimerBack;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private SpriteRenderer rendererPenaltyPanel;

    // カメラ

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectMainCamera;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Camera cameraMain;

    // ボタン

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectDropButton, objectDropButtonText;
    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectReplaceButton, objectReplaceButtonText;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageDropButton, imageReplaceButton;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text textDropButton, textReplaceButton;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectRetryInGameButton, objectRetryInGameButtonIcon;
    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectTitleButton, objectTitleButtonIcon;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageRetryInGameButton, imageTitleButton;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageRetryInGameButtonIcon, imageTitleButtonIcon;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectTweetButton, objectTweetButtonText, objectTweetButtonIcon;
    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectRankingButton, objectRankingButtonText, objectRankingButtonIcon;
    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectRetryButton, objectRetryButtonText, objectRetryButtonIcon;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageTweetButton, imageRankingButton, imageRetryButton;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text textTweetButton, textRankingButton, textRetryButton;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageTweetButtonIcon, imageRankingButtonIcon, imageRetryButtonIcon;

    // テキスト

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectPhaseScoreHeading;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text textPhaseScoreHeading;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text[] textPhaseScores = new Text[10];
    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectTextTotalScoreHeading, objectTextTotalScoreValue;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text textTotalScoreHeading, textTotalScoreValue;
    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectTextRank, objectTextNextRankl;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text textRank, textNextRank;
    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectTextTipsHeading, objectTextTipsValue;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text textTipsHeading, textTipsValue;

    // 背景

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectBackgroundPanels, objectBackgroundPhaseScore, objectBackgroundTotalScore;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private SpriteRenderer rendererBackgroundPanels, rendererBackgroundPhaseScore, rendererBackgroundTotalScore;

    // 定数

    [BoxGroup("Constant")]
    public readonly int NumOfColumns = 8;
    [BoxGroup("Constant")]
    public readonly int NumOfLines = 8;
    [BoxGroup("Constant")]
    public readonly int NumOfPanelType = 2;
    [BoxGroup("Constant")]
    public readonly float PanelWidth = 1.0f;
    [BoxGroup("Constant")]
    public readonly float PanelHeight = 1.0f;


    [BoxGroup("State"), ShowInInspector, ReadOnly]
    private bool isRunning;
    public bool IsRunning {
        get {
            return isRunning;
        }
        set {
            isRunning = value;
        }
    }

    // ステータス

    [BoxGroup("State"), ShowInInspector, ReadOnly]
    private int[,] panelsId = new int[8, 8];

    public int GetPanelsId(int column, int line) {
        return panelsId[column, line];
    }

    public void SetPanelsId(int column, int line, int id) {
        if (id < 0) {
            id = 0;
        } else if (id > NumOfPanelType) {
            id = NumOfPanelType;
        }

        panelsId[column, line] = id;
    }

    [BoxGroup("State"), ShowInInspector, ReadOnly]
    private int phase;
    public int Phase {
        get {
            return phase;
        }
        set {
            if (value < 1) {
                phase = 1;
            } else if (value > 10) {
                phase = 10;
            } else {
                phase = value;
            }
        }
    }

    [BoxGroup("State"), ShowInInspector, ReadOnly]
    private bool hasSelected;
    public bool HasSelected {
        get {
            return hasSelected;
        }
        set {
            hasSelected = value;
            UpdateColorOfControllButton();
        }
    }

    // タイマー

    [BoxGroup("Timer"), ShowInInspector, ReadOnly]
    private float timer, timerPenalty;
    public float Timer {
        get {
            return timer;
        }
        set {
            timer = value;

            if (value < 0) {
                timer = 0;
            } else {
                timer = value;
            }
        }
    }
    public float TimerPenalty {
        get {
            return timerPenalty;
        }
        set {
            timerPenalty = value;

            if (value < 0) {
                timerPenalty = 0;
            } else {
                timerPenalty = value;
            }
        }
    }

    [BoxGroup("Timer"), ShowInInspector, ReadOnly]
    private bool flagTimeSE;

    // Awake Start Update

    private void Awake() {
        this.scriptScore = this.GetComponent<Score>();
        this.scriptVaryPanel = this.GetComponent<VaryPanel>();

        this.objectOptionManager = GameObject.FindWithTag("OptionManager");
        this.scriptGameOption = this.objectOptionManager.GetComponent<GameOption>();

        this.objectBGMManager = GameObject.FindWithTag("BGMManager");
        this.scriptBGMManager = this.objectBGMManager.GetComponent<BGMManager>();

        this.objectSEManager = GameObject.FindWithTag("SEManager");
        this.scriptSEManager = this.objectSEManager.GetComponent<SEManager>();

        this.imageDropButton = this.objectDropButton.GetComponent<Image>();
        this.imageReplaceButton = this.objectReplaceButton.GetComponent<Image>();
        this.textDropButton = this.objectDropButtonText.GetComponent<Text>();
        this.textReplaceButton = this.objectReplaceButtonText.GetComponent<Text>();

        this.imageRetryInGameButton = this.objectRetryInGameButton.GetComponent<Image>();
        this.imageTitleButton = this.objectTitleButton.GetComponent<Image>();
        this.imageRetryInGameButtonIcon = this.objectRetryInGameButtonIcon.GetComponent<Image>();
        this.imageTitleButtonIcon = this.objectTitleButtonIcon.GetComponent<Image>();

        this.imageTweetButton = this.objectTweetButton.GetComponent<Image>();
        this.imageRankingButton = this.objectRankingButton.GetComponent<Image>();
        this.imageRetryButton = this.objectRetryButton.GetComponent<Image>();
        this.textTweetButton = this.objectTweetButtonText.GetComponent<Text>();
        this.textRankingButton = this.objectRankingButtonText.GetComponent<Text>();
        this.textRetryButton = this.objectRetryButtonText.GetComponent<Text>();
        this.imageTweetButtonIcon = this.objectTweetButtonIcon.GetComponent<Image>();
        this.imageRankingButtonIcon = this.objectRankingButtonIcon.GetComponent<Image>();
        this.imageRetryButtonIcon = this.objectRetryButtonIcon.GetComponent<Image>();

        this.scriptGameButton = this.objectCanvasButton.GetComponent<GameButton>();
        this.textTimer = this.objectTextTimer.GetComponent<Text>();
        this.imageTimer = this.objectImageTimer.GetComponent<Image>();
        this.imageTimerBack = this.objectImageTimerBack.GetComponent<Image>();
        this.rendererPenaltyPanel = this.objectPenaltyPanel.GetComponent<SpriteRenderer>();

        this.cameraMain = this.objectMainCamera.GetComponent<Camera>();

        this.rendererBackgroundPanels = this.objectBackgroundPanels.GetComponent<SpriteRenderer>();
        this.rendererBackgroundPhaseScore = this.objectBackgroundPhaseScore.GetComponent<SpriteRenderer>();
        this.rendererBackgroundTotalScore = this.objectBackgroundTotalScore.GetComponent<SpriteRenderer>();

        this.textPhaseScoreHeading = this.objectPhaseScoreHeading.GetComponent<Text>();
        this.textTotalScoreHeading = this.objectTextTotalScoreHeading.GetComponent<Text>();
        this.textTotalScoreValue = this.objectTextTotalScoreValue.GetComponent<Text>();
        this.textRank = this.objectTextRank.GetComponent<Text>();
        this.textNextRank = this.objectTextNextRankl.GetComponent<Text>();
        this.textTipsHeading = this.objectTextTipsHeading.GetComponent<Text>();
        this.textTipsValue = this.objectTextTipsValue.GetComponent<Text>();

        int indexPhaseScore = 0;
        foreach (Transform transformPhaseScore in this.objectPhaseScoreHeading.transform) {
            this.textPhaseScores[indexPhaseScore] = transformPhaseScore.gameObject.GetComponent<Text>();
            indexPhaseScore++;
        }

        this.IsRunning = false;
        this.phase = 1;
        this.HasSelected = false;

        this.Timer = 10;
        this.TimerPenalty = 0;
        this.flagTimeSE = false;
    }

    private void Start() {
        UpdateColors();

        StartPhase();
    }

    private void Update() {
        if (this.IsRunning) {
            this.Timer -= Time.deltaTime;
            this.TimerPenalty -= Time.deltaTime;
            UpdateTimer();

            if (this.Timer <= 1) {
                if (!this.flagTimeSE) {
                    this.scriptSEManager.PlaySEAlerm();
                    this.flagTimeSE = true;
                }


            } else if (this.Timer <= 2) {
                if (this.flagTimeSE) {
                    this.scriptSEManager.PlaySEAlerm();
                    this.flagTimeSE = false;
                }

            } else if (this.Timer <= 3) {
                if (!this.flagTimeSE) {
                    this.scriptSEManager.PlaySEAlerm();
                    this.flagTimeSE = true;
                }

            }
        }

        if (this.TimerPenalty > 0) {
            this.objectPenaltyPanel.SetActive(true);
        } else {
            this.objectPenaltyPanel.SetActive(false);
        }

        if (this.Timer <= 0) {
            TimeZero();
        }

        // デバッグ用
        if (this.scriptGameOption.DebugMode) {
            if (Input.GetKeyDown(KeyCode.RightShift)) {
                TimeZero();
            }
        }
    }

    // 時間管理

    private void UpdateTimer() {
        this.textTimer.text = System.Math.Ceiling(this.Timer).ToString();
        this.imageTimer.fillAmount = this.Timer / 10;
    }

    private void TimeZero() {
        if (this.Phase >= 10) {
            GameFinish();
        } else {
            NextPhase();
        }
    }

    // フェーズ管理

    private void NextPhase() {
        this.IsRunning = false;

        this.scriptVaryPanel.DropPanels();
        this.scriptVaryPanel.FillPanels();
        ResetPanels();

        this.Timer = 10;
        this.TimerPenalty = 0;
        this.flagTimeSE = false;

        UpdateTimer();

        this.scriptScore.SelectCount = 0;

        this.Phase++;
        StartPhase();
    }

    private void StartPhase() {
        if (this.Phase == 1) {
            this.scriptBGMManager.PlayMusicNewScene();
            SetPanels();

        } else if (this.Phase == 6) {
            this.scriptBGMManager.SwitchGameMusic();
        }

        this.IsRunning = true;
    }

    private void GameFinish() {
        this.IsRunning = false;

        this.scriptBGMManager.PlayResultMusic();

        this.scriptVaryPanel.DropPanels();
        this.scriptVaryPanel.FillPanels();
        DestoryPanels();

        this.scriptGameButton.SwitchActiveButtonResult();
        this.scriptScore.OutputRank();

        this.Timer = 10;
        this.TimerPenalty = 0;
        this.textTimer.text = "";
        this.imageTimer.fillAmount = 0;

        this.scriptScore.SelectCount = 0;

        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(this.scriptScore.GetTotalScore());
    }

    // パネル管理

    public void ResetPanels() {
        DestoryPanels();
        SetPanels();
    }

    void DestoryPanels() {
        foreach (Transform panel in this.objectPanels.transform) {
            Destroy(panel.gameObject);
        }

    }

    public void SetPanel(int column, int line) {
        panelsId[column, line] = Random.Range(1, NumOfPanelType + 1);

        float positionX = (PanelWidth * column) - 8.2f;
        float positionY = (PanelHeight * line) - 4.2f;

        InstantiatePanel(panelsId[column, line], column, line, positionX, positionY);
    }

    private void SetPanels() {
        for (int colomn = 0; colomn < NumOfColumns; colomn++) {
            for (int line = 0; line < NumOfLines; line++) {
                SetPanel(colomn, line);
            }
        }
    }

    private void DebugPanels() {
        // デバッグ用

        for (int colomn = 0; colomn < NumOfColumns; colomn++) {
            for (int line = 0; line < NumOfLines; line++) {
                string color = "";
                if (this.panelsId[colomn, line] == 1) color = "red";
                if (this.panelsId[colomn, line] == 2) color = "yellow";
                if (this.panelsId[colomn, line] == 3) color = "green";

                Debug.Log("DebugPanels：" + colomn + "," + line + " is " + color);
            }
        }
    }

    private void InstantiatePanel(int id, int column, int line, float positionX, float positionY) {
        if (id == 0) {
            return;
        } else if (id >= 3) {
            return;
        }

        GameObject panel = Instantiate(prefabPanel) as GameObject;

        switch (id) {
            case 1:
                panel.GetComponent<SpriteRenderer>().color = this.scriptGameOption.ColorPanelA;
                break;
            case 2:
                panel.GetComponent<SpriteRenderer>().color = this.scriptGameOption.ColorPanelB;
                break;
        }

        panel.transform.position = new Vector3(positionX, positionY, 0);
        panel.name = column.ToString() + " " + line.ToString();
        panel.transform.SetParent(this.objectPanels.transform);
    }

    // 配色反映

    public void UpdateColors() {
        UpdateColorOfMainCamera();
        UpdateColorOfBackground();
        UpdateColorOfButton();
        UpdateColorOfTimer();
        UpdateColorOfText();
    }

    private void UpdateColorOfMainCamera() {
        this.cameraMain.backgroundColor = this.scriptGameOption.ColorCamera;
    }
    
    private void UpdateColorOfBackground() {
        this.rendererBackgroundPanels.color = this.scriptGameOption.ColorBackground;
        this.rendererBackgroundPhaseScore.color = this.scriptGameOption.ColorPanelA;
        this.rendererBackgroundTotalScore.color = this.scriptGameOption.ColorPanelB;
    }

    private void UpdateColorOfButton() {
        UpdateColorOfControllButton();

        this.imageTweetButton.color = this.scriptGameOption.ColorPanelA;
        this.textTweetButton.color = this.scriptGameOption.ColorTextMain;
        this.imageTweetButtonIcon.color = this.scriptGameOption.ColorTextMain;

        this.imageRankingButton.color = this.scriptGameOption.ColorPanelB;
        this.textRankingButton.color = this.scriptGameOption.ColorTextMain;
        this.imageRankingButtonIcon.color = this.scriptGameOption.ColorTextMain;

        this.imageRetryButton.color = this.scriptGameOption.ColorAccent;
        this.textRetryButton.color = this.scriptGameOption.ColorTextMain;
        this.imageRetryButtonIcon.color = this.scriptGameOption.ColorTextMain;

        if (this.scriptGameOption.OptionColorType == GameOption.ColorType.Original) {
            this.imageTitleButton.color = this.scriptGameOption.ColorAccent;
            this.imageTitleButtonIcon.color = this.scriptGameOption.ColorTextMain;
            this.imageRetryInGameButton.color = this.scriptGameOption.ColorAccent;
            this.imageRetryInGameButtonIcon.color = this.scriptGameOption.ColorTextMain;

        } else {
            this.imageTitleButton.color = this.scriptGameOption.ColorTextMain;
            this.imageTitleButtonIcon.color = this.scriptGameOption.ColorAccent;
            this.imageRetryInGameButton.color = this.scriptGameOption.ColorTextMain;
            this.imageRetryInGameButtonIcon.color = this.scriptGameOption.ColorAccent;
        }
    }

    public void UpdateColorOfControllButton() {
        if (this.scriptGameOption.OptionColorType == GameOption.ColorType.LightCD) {
            this.textDropButton.color = this.scriptGameOption.ColorPanelB;
            this.textReplaceButton.color = this.scriptGameOption.ColorPanelB;
        } else {
            this.textDropButton.color = this.scriptGameOption.ColorTextMain;
            this.textReplaceButton.color = this.scriptGameOption.ColorTextMain;
        }

        Color color = this.scriptGameOption.ColorAccent;
        byte r = (byte)(color.r * 255);
        byte g = (byte)(color.g * 255);
        byte b = (byte)(color.b * 255);

        if (this.scriptGameOption.DebugMode) {
            Debug.Log("R:" + r + " G:" + g + " B:" + b);
        }

        if (this.hasSelected) {
            this.imageDropButton.color = new Color32(r, g, b, 255);
            this.imageReplaceButton.color = new Color32(r, g, b, 64);

        } else {
            this.imageDropButton.color = new Color32(r, g, b, 64);
            this.imageReplaceButton.color = new Color32(r, g, b, 255);

        }
    }

    private void UpdateColorOfTimer() {
        this.rendererPenaltyPanel.color = this.scriptGameOption.ColorBackground;
        this.textTimer.color = this.scriptGameOption.ColorTextMain;

        if (this.scriptGameOption.OptionColorType == GameOption.ColorType.LightCH) {
            this.imageTimer.color = this.scriptGameOption.ColorTimerBack;
            this.imageTimerBack.color = this.scriptGameOption.ColorAccent;
        } else {
            this.imageTimer.color = this.scriptGameOption.ColorAccent;
            this.imageTimerBack.color = this.scriptGameOption.ColorTimerBack;
        }
    }

    private void UpdateColorOfText() {
        this.textPhaseScoreHeading.color = this.scriptGameOption.ColorTextMain;
        this.textTotalScoreHeading.color = this.scriptGameOption.ColorTextMain;
        this.textTotalScoreValue.color = this.scriptGameOption.ColorTextMain;

        if (this.scriptGameOption.OptionColorType == GameOption.ColorType.Original) {
            this.textRank.color = this.scriptGameOption.ColorTextSub;
            this.textNextRank.color = this.scriptGameOption.ColorTextSub;
            this.textTipsHeading.color = this.scriptGameOption.ColorTextSub;
            this.textTipsValue.color = this.scriptGameOption.ColorTextSub;

        } else {
            this.textRank.color = this.scriptGameOption.ColorTextMain;
            this.textNextRank.color = this.scriptGameOption.ColorTextMain;
            this.textTipsHeading.color = this.scriptGameOption.ColorTextMain;
            this.textTipsValue.color = this.scriptGameOption.ColorTextMain;

        }

        foreach (Text textPhaseScoreValue in this.textPhaseScores) {
            textPhaseScoreValue.color = this.scriptGameOption.ColorTextMain;
        }
    }
}
