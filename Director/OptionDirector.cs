using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class OptionDirector : MonoBehaviour {

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

    // スプライト

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectBackgroundPanels, objectBackgroundPhaseScore, objectBackgroundTotalScore, objectBackgroundOption;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private SpriteRenderer rendererBackgroundPanels, rendererBackgroundPhaseScore, rendererBackgroundTotalScore, rendererBackgroundOption;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectSamplePanels;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private SpriteRenderer[] rendererSamplePanels = new SpriteRenderer[9];

    // ボタン

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectTitleButton, objectTitleButtonIcon;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageTitleButton, imageTitleButtonIcon;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectThemeMenuButton, objectThemeMenuButtonIcon, objectSoundMenuButton, objectSoundMenuButtonIcon, objectSelectedButtonBackground;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageThemeMenuButton, imageThemeMenuButtonIcon, imageSoundMenuButton, imageSoundMenuButtonIcon, imageSelectedButtonBackground;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectSwitchThemeButton, objectSwitchThemeButtonIcon;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageSwitchThemeButton, imageSwitchThemeButtonIcon;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectBGMTestButton, objectBGMTestButtonText;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageBGMTestButton;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text textBGMTestButton;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectSETestButton, objectSETestButtonText;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageSETestButton;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text textSETestButton;

    public enum OptionMenu {
        Theme, Sound
    }

    // スライダー

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectSliderBGMVolume, objectSliderSEVolume;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectSliderBGMVolumeIcon, objectSliderSEVolumeIcon;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageSliderBGMVolumeIcon, imageSliderSEVolumeIcon;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectSliderBGMVolumeBackground, objectSliderSEVolumeBackground;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageSliderBGMVolumeBackground, imageSliderSEVolumeBackground;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectSliderBGMVolumeFill, objectSliderSEVolumeFill;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageSliderBGMVolumeFill, imageSliderSEVolumeFill;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectSliderBGMVolumeHandleEdge, objectSliderSEVolumeHandleEdge;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageSliderBGMVolumeHandleEdge, imageSliderSEVolumeHandleEdge;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectSliderBGMVolumeHandleCore, objectSliderSEVolumeHandleCore;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageSliderBGMVolumeHandleCore, imageSliderSEVolumeHandleCore;

    // テキスト

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectTextOptionHeader, objectTextSwitchThemeHeader, objectTextPreview, objectTextNote;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text textOptionHeader, textSwitchThemeHeader, textPreview, textNote;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectTextBGMVolume, objectTextSEVolume;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text textBGMVolume, textSEVolume;

    // Awake Start Update

    private void Awake() {
        this.objectOptionManager = GameObject.FindWithTag("OptionManager");
        this.scriptGameOption = this.objectOptionManager.GetComponent<GameOption>();

        this.objectBGMManager = GameObject.FindWithTag("BGMManager");
        this.scriptBGMManager = this.objectBGMManager.GetComponent<BGMManager>();

        this.cameraMain = this.objectMainCamera.GetComponent<Camera>();

        this.rendererBackgroundPanels = this.objectBackgroundPanels.GetComponent<SpriteRenderer>();
        this.rendererBackgroundPhaseScore = this.objectBackgroundPhaseScore.GetComponent<SpriteRenderer>();
        this.rendererBackgroundTotalScore = this.objectBackgroundTotalScore.GetComponent<SpriteRenderer>();
        this.rendererBackgroundOption = this.objectBackgroundOption.GetComponent<SpriteRenderer>();

        int indexSamplePanel = 0;
        foreach (Transform samplePanel in this.objectSamplePanels.transform) {
            this.rendererSamplePanels[indexSamplePanel] = samplePanel.gameObject.GetComponent<SpriteRenderer>();
            indexSamplePanel++;
        }

        this.imageTitleButton = this.objectTitleButton.GetComponent<Image>();
        this.imageTitleButtonIcon = this.objectTitleButtonIcon.GetComponent<Image>();
        this.imageThemeMenuButton = this.objectThemeMenuButton.GetComponent<Image>();
        this.imageThemeMenuButtonIcon = this.objectThemeMenuButtonIcon.GetComponent<Image>();
        this.imageSoundMenuButton = this.objectSoundMenuButton.GetComponent<Image>();
        this.imageSoundMenuButtonIcon = this.objectSoundMenuButtonIcon.GetComponent<Image>();
        this.imageSelectedButtonBackground = this.objectSelectedButtonBackground.GetComponent<Image>();

        this.imageSwitchThemeButton = this.objectSwitchThemeButton.GetComponent<Image>();
        this.imageSwitchThemeButtonIcon = this.objectSwitchThemeButtonIcon.GetComponent<Image>();

        this.imageBGMTestButton = this.objectBGMTestButton.GetComponent<Image>();
        this.textBGMTestButton = this.objectBGMTestButtonText.GetComponent<Text>();
        this.imageSETestButton = this.objectSETestButton.GetComponent<Image>();
        this.textSETestButton = this.objectSETestButtonText.GetComponent<Text>();

        this.imageSliderBGMVolumeIcon = this.objectSliderBGMVolumeIcon.GetComponent<Image>();
        this.imageSliderSEVolumeIcon = this.objectSliderSEVolumeIcon.GetComponent<Image>();
        this.imageSliderBGMVolumeBackground = this.objectSliderBGMVolumeBackground.GetComponent<Image>();
        this.imageSliderSEVolumeBackground = this.objectSliderSEVolumeBackground.GetComponent<Image>();
        this.imageSliderBGMVolumeFill = this.objectSliderBGMVolumeFill.GetComponent<Image>();
        this.imageSliderSEVolumeFill = this.objectSliderSEVolumeFill.GetComponent<Image>();
        this.imageSliderBGMVolumeHandleEdge = this.objectSliderBGMVolumeHandleEdge.GetComponent<Image>();
        this.imageSliderSEVolumeHandleEdge = this.objectSliderSEVolumeHandleEdge.GetComponent<Image>();
        this.imageSliderBGMVolumeHandleCore = this.objectSliderBGMVolumeHandleCore.GetComponent<Image>();
        this.imageSliderSEVolumeHandleCore = this.objectSliderSEVolumeHandleCore.GetComponent<Image>();

        this.textOptionHeader = this.objectTextOptionHeader.GetComponent<Text>();
        this.textSwitchThemeHeader = this.objectTextSwitchThemeHeader.GetComponent<Text>();
        this.textPreview = this.objectTextPreview.GetComponent<Text>();
        this.textNote = this.objectTextNote.GetComponent<Text>();

        this.textBGMVolume = this.objectTextBGMVolume.GetComponent<Text>();
        this.textSEVolume = this.objectTextSEVolume.GetComponent<Text>();
    }

    private void Start() {
        UpdateColors();
        UpdateSwitchThemeHeader();
        UpdateOptionMenu(OptionMenu.Theme);
        this.scriptBGMManager.PlayMusicNewScene();
    }

    // 配色反映

    public void UpdateColors() {
        UpdateColorOfMainCamera();
        UpdateColorOfBackground();
        UpdateColorOfSamplePanels();
        UpdateColorOfButton();
        UpdateColorOfSlider();
        UpdateColorOfText();
    }

    private void UpdateColorOfMainCamera() {
        this.cameraMain.backgroundColor = this.scriptGameOption.ColorCamera;
    }

    private void UpdateColorOfBackground() {
        this.rendererBackgroundPanels.color = this.scriptGameOption.ColorBackground;
        this.rendererBackgroundPhaseScore.color = this.scriptGameOption.ColorPanelA;
        this.rendererBackgroundTotalScore.color = this.scriptGameOption.ColorPanelB;
        this.rendererBackgroundOption.color = this.scriptGameOption.ColorBackground;
    }

    private void UpdateColorOfSamplePanels() {
        foreach (SpriteRenderer rendererSamplePanel in this.rendererSamplePanels) {
            if (rendererSamplePanel.gameObject.name == "Green") {
                rendererSamplePanel.color = this.scriptGameOption.ColorPanelA;
            } else {
                rendererSamplePanel.color = this.scriptGameOption.ColorPanelB;
            }
        }
    }

    private void UpdateColorOfButton() {
        if (this.scriptGameOption.OptionColorType == GameOption.ColorType.Original) {
            this.imageTitleButton.color = this.scriptGameOption.ColorAccent;
            this.imageTitleButtonIcon.color = this.scriptGameOption.ColorTextMain;

            this.imageSelectedButtonBackground.color = this.scriptGameOption.ColorTextSub;
            this.imageSwitchThemeButtonIcon.color = this.scriptGameOption.ColorTextSub;

        } else {
            this.imageTitleButton.color = this.scriptGameOption.ColorTextMain;
            this.imageTitleButtonIcon.color = this.scriptGameOption.ColorAccent;

            this.imageSelectedButtonBackground.color = this.scriptGameOption.ColorTextMain;
            this.imageSwitchThemeButtonIcon.color = this.scriptGameOption.ColorTextMain;
        }

        if (this.scriptGameOption.OptionColorType == GameOption.ColorType.LightCD) {
            this.textBGMTestButton.color = this.scriptGameOption.ColorPanelB;
            this.textSETestButton.color = this.scriptGameOption.ColorPanelB;
        } else {
            this.textBGMTestButton.color = this.scriptGameOption.ColorTextMain;
            this.textSETestButton.color = this.scriptGameOption.ColorTextMain;
        }

        this.imageThemeMenuButton.color = this.scriptGameOption.ColorPanelA;
        this.imageThemeMenuButtonIcon.color = this.scriptGameOption.ColorTextMain;
        this.imageSoundMenuButton.color = this.scriptGameOption.ColorPanelB;
        this.imageSoundMenuButtonIcon.color = this.scriptGameOption.ColorTextMain;

        this.imageSwitchThemeButton.color = this.scriptGameOption.ColorCamera;

        this.imageBGMTestButton.color = this.scriptGameOption.ColorAccent;
        this.imageSETestButton.color = this.scriptGameOption.ColorAccent;

    }

    private void UpdateColorOfSlider() {
        if (this.scriptGameOption.OptionColorType == GameOption.ColorType.Original) {
            this.imageSliderBGMVolumeIcon.color = this.scriptGameOption.ColorTextSub;
            this.imageSliderSEVolumeIcon.color = this.scriptGameOption.ColorTextSub;
            this.imageSliderBGMVolumeHandleEdge.color = this.scriptGameOption.ColorTextSub;
            this.imageSliderSEVolumeHandleEdge.color = this.scriptGameOption.ColorTextSub;

        } else {
            this.imageSliderBGMVolumeIcon.color = this.scriptGameOption.ColorTextMain;
            this.imageSliderSEVolumeIcon.color = this.scriptGameOption.ColorTextMain;
            this.imageSliderBGMVolumeHandleEdge.color = this.scriptGameOption.ColorTextMain;
            this.imageSliderSEVolumeHandleEdge.color = this.scriptGameOption.ColorTextMain;
        }

        this.imageSliderBGMVolumeBackground.color = this.scriptGameOption.ColorCamera;
        this.imageSliderSEVolumeBackground.color = this.scriptGameOption.ColorCamera;
        this.imageSliderBGMVolumeFill.color = this.scriptGameOption.ColorPanelA;
        this.imageSliderSEVolumeFill.color = this.scriptGameOption.ColorPanelA;
        this.imageSliderBGMVolumeHandleCore.color = this.scriptGameOption.ColorAccent;
        this.imageSliderSEVolumeHandleCore.color = this.scriptGameOption.ColorAccent;
    }

    private void UpdateColorOfText() {
        if (this.scriptGameOption.OptionColorType == GameOption.ColorType.Original) {
            this.textOptionHeader.color = this.scriptGameOption.ColorTextSub;
            this.textSwitchThemeHeader.color = this.scriptGameOption.ColorTextSub;
            this.textPreview.color = this.scriptGameOption.ColorTextSub;
            this.textBGMVolume.color = this.scriptGameOption.ColorTextSub;
            this.textSEVolume.color = this.scriptGameOption.ColorTextSub;
            this.textNote.color = this.scriptGameOption.ColorTextSub;
        } else {
            this.textOptionHeader.color = this.scriptGameOption.ColorTextMain;
            this.textSwitchThemeHeader.color = this.scriptGameOption.ColorTextMain;
            this.textPreview.color = this.scriptGameOption.ColorTextMain;
            this.textBGMVolume.color = this.scriptGameOption.ColorTextMain;
            this.textSEVolume.color = this.scriptGameOption.ColorTextMain;
            this.textNote.color = this.scriptGameOption.ColorTextMain;
        }
    }

    // メニュー

    public void UpdateOptionMenu(OptionMenu optionMenu) {
        if (optionMenu != OptionMenu.Theme) {
            this.objectSwitchThemeButton.SetActive(false);
            this.objectTextSwitchThemeHeader.SetActive(false);
            this.objectSamplePanels.SetActive(false);
        }

        if (optionMenu != OptionMenu.Sound) {
            this.objectSliderBGMVolume.SetActive(false);
            this.objectSliderSEVolume.SetActive(false);
            this.objectBGMTestButton.SetActive(false);
            this.objectSETestButton.SetActive(false);
        }

        if (optionMenu == OptionMenu.Theme) {
            this.textOptionHeader.text = "見た目を変更";
            this.textPreview.text = "プレビュー";
            this.objectSelectedButtonBackground.transform.position = this.objectThemeMenuButton.transform.position;
            this.objectSwitchThemeButton.SetActive(true);
            this.objectTextSwitchThemeHeader.SetActive(true);
            this.objectSamplePanels.SetActive(true);
        }

        if (optionMenu == OptionMenu.Sound) {
            this.textOptionHeader.text = "音量を変更";
            this.textPreview.text = "サウンドテスト";
            this.objectSelectedButtonBackground.transform.position = this.objectSoundMenuButton.transform.position;
            this.objectSliderBGMVolume.SetActive(true);
            this.objectSliderSEVolume.SetActive(true);
            this.objectBGMTestButton.SetActive(true);
            this.objectSETestButton.SetActive(true);
        }
    }

    // 配色変更

    public void SwitchTheme() {
        this.scriptGameOption.SwitchColorType();
        UpdateColors();
        UpdateSwitchThemeHeader();
    }

    private void UpdateSwitchThemeHeader() {
        this.textSwitchThemeHeader.text = "テーマ: " + this.scriptGameOption.GetColorTypeName();
    }
}
