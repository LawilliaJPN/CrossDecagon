using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class TitleDirector :MonoBehaviour {

    // スクリプト

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectOptionManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameOption scriptGameOption;

    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameObject objectBGMManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private BGMManager scriptBGMManager;

    // カメラ

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectMainCamera;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Camera cameraMain;

    // 背景

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectBackgroundPanels, objectBackgroundPhaseScore, objectBackgroundTotalScore;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private SpriteRenderer rendererBackgroundPanels, rendererBackgroundPhaseScore, rendererBackgroundTotalScore;

    // ボタン

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectHowToPlayButton, objectHowToPlayButtonText, objectHowToPlayButtonIcon;
    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectStartButton, objectStartButtonText, objectStartButtonIcon;
    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectRankingButton, objectRankingButtonText, objectRankingButtonIcon;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageHowToPlay, imageStart, imageRanking;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text textHowToPlay, textStart, textRanking;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageHowToPlayIcon, imageStartIcon, imageRankingIcon;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectOptionButton, objectOptionButtonIcon;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageOptionButton, imageOptionButtonIcon;

    // テキスト

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectTextTitle, objectTextCopyright, objectTextVersion, objectTextDebug;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private TextMeshProUGUI textMeshTitle;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text textCopyright, textVersion, textDebug;

    // Awake Start Update

    private void Awake() {
        this.objectOptionManager = GameObject.FindWithTag("OptionManager");
        this.scriptGameOption = this.objectOptionManager.GetComponent<GameOption>();

        this.objectBGMManager = GameObject.FindWithTag("BGMManager");
        this.scriptBGMManager = this.objectBGMManager.GetComponent<BGMManager>();

        this.cameraMain = this.objectMainCamera.GetComponent<Camera>();

        this.imageHowToPlay = this.objectHowToPlayButton.GetComponent<Image>();
        this.imageStart = this.objectStartButton.GetComponent<Image>();
        this.imageRanking = this.objectRankingButton.GetComponent<Image>();
        this.textHowToPlay = this.objectHowToPlayButtonText.GetComponent<Text>();
        this.textStart = this.objectStartButtonText.GetComponent<Text>();
        this.textRanking = this.objectRankingButtonText.GetComponent<Text>();
        this.imageHowToPlayIcon = this.objectHowToPlayButtonIcon.GetComponent<Image>();
        this.imageStartIcon = this.objectStartButtonIcon.GetComponent<Image>();
        this.imageRankingIcon = this.objectRankingButtonIcon.GetComponent<Image>();

        this.imageOptionButton = this.objectOptionButton.GetComponent<Image>();
        this.imageOptionButtonIcon = this.objectOptionButtonIcon.GetComponent<Image>();

        this.rendererBackgroundPanels = this.objectBackgroundPanels.GetComponent<SpriteRenderer>();
        this.rendererBackgroundPhaseScore = this.objectBackgroundPhaseScore.GetComponent<SpriteRenderer>();
        this.rendererBackgroundTotalScore = this.objectBackgroundTotalScore.GetComponent<SpriteRenderer>();

        this.textMeshTitle = this.objectTextTitle.GetComponent<TextMeshProUGUI>();
        this.textCopyright = this.objectTextCopyright.GetComponent<Text>();
        this.textVersion = this.objectTextVersion.GetComponent<Text>();
        this.textDebug = this.objectTextDebug.GetComponent<Text>();
    }

    private void Start() {
        UpdateColors();
        this.scriptBGMManager.PlayMusicNewScene();
    }

    // 配色反映

    public void UpdateColors() {
        UpdateColorOfMainCamera();
        UpdateColorOfBackground();
        UpdateColorOfCanvasText();
        UpdateColorOfCanvasButton();
    }

    private void UpdateColorOfMainCamera() {
        this.cameraMain.backgroundColor = this.scriptGameOption.ColorCamera;
    }

    private void UpdateColorOfBackground() {
        this.rendererBackgroundPanels.color = this.scriptGameOption.ColorBackground;
        this.rendererBackgroundPhaseScore.color = this.scriptGameOption.ColorPanelA;
        this.rendererBackgroundTotalScore.color = this.scriptGameOption.ColorPanelB;
    }

    private void UpdateColorOfCanvasText() {
        this.textMeshTitle.color = this.scriptGameOption.ColorAccent;
        this.textMeshTitle.faceColor = this.scriptGameOption.ColorAccent;

        if (this.scriptGameOption.OptionColorType == GameOption.ColorType.Original) {
            this.textMeshTitle.outlineColor = this.scriptGameOption.ColorTextSub;

            this.textCopyright.color = this.scriptGameOption.ColorTextSub;
            this.textVersion.color = this.scriptGameOption.ColorTextSub;
            this.textDebug.color = this.scriptGameOption.ColorTextSub;

        } else {
            this.textMeshTitle.outlineColor = this.scriptGameOption.ColorTextMain;

            this.textCopyright.color = this.scriptGameOption.ColorTextMain;
            this.textVersion.color = this.scriptGameOption.ColorTextMain;
            this.textDebug.color = this.scriptGameOption.ColorTextMain;
        }
    }

    private void UpdateColorOfCanvasButton() {
        if (this.scriptGameOption.OptionColorType == GameOption.ColorType.Original) {
            this.imageOptionButton.color = this.scriptGameOption.ColorAccent;
            this.imageOptionButtonIcon.color = this.scriptGameOption.ColorTextMain;

        } else {
            this.imageOptionButton.color = this.scriptGameOption.ColorTextMain;
            this.imageOptionButtonIcon.color = this.scriptGameOption.ColorAccent;
        }

        this.imageStart.color = this.scriptGameOption.ColorAccent;
        this.textStart.color = this.scriptGameOption.ColorTextMain;
        this.imageStartIcon.color = this.scriptGameOption.ColorTextMain;

        this.imageRanking.color = this.scriptGameOption.ColorPanelB;
        this.textRanking.color = this.scriptGameOption.ColorTextMain;
        this.imageRankingIcon.color = this.scriptGameOption.ColorTextMain;

        this.imageHowToPlay.color = this.scriptGameOption.ColorPanelA;
        this.textHowToPlay.color = this.scriptGameOption.ColorTextMain;
        this.imageHowToPlayIcon.color = this.scriptGameOption.ColorTextMain;
    }
}
