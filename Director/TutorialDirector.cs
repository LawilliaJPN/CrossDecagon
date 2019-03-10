using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class TutorialDirector : MonoBehaviour {
    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectOptionManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameOption scriptGameOption;

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectBGMManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private BGMManager scriptBGMManager;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectMainCamera;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Camera cameraMain;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectBackgroundPanels, objectBackgroundPhaseScore, objectBackgroundTotalScore, objectBackgroundTutorial;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private SpriteRenderer rendererBackgroundPanels, rendererBackgroundPhaseScore, rendererBackgroundTotalScore, rendererBackgroundTutorial;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectTitleButton, objectTitleButtonIcon;
    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectNextButton, objectNextButtonIcon;
    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectPreviousButton, objectPreviousButtonIcon;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageTitle, imageNext, imagePrevious;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageTitleIcon, imageNextIcon, imagePreviousIcon;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectTextSlideNumber;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text textSlideNumber;

    private void Awake() {
        this.objectOptionManager = GameObject.FindWithTag("OptionManager");
        this.scriptGameOption = this.objectOptionManager.GetComponent<GameOption>();

        this.objectBGMManager = GameObject.FindWithTag("BGMManager");
        this.scriptBGMManager = this.objectBGMManager.GetComponent<BGMManager>();

        this.cameraMain = this.objectMainCamera.GetComponent<Camera>();

        this.imageTitle = this.objectTitleButton.GetComponent<Image>();
        this.imageNext = this.objectNextButton.GetComponent<Image>();
        this.imagePrevious = this.objectPreviousButton.GetComponent<Image>();
        this.imageTitleIcon = this.objectTitleButtonIcon.GetComponent<Image>();
        this.imageNextIcon = this.objectNextButtonIcon.GetComponent<Image>();
        this.imagePreviousIcon = this.objectPreviousButtonIcon.GetComponent<Image>();

        this.rendererBackgroundPanels = this.objectBackgroundPanels.GetComponent<SpriteRenderer>();
        this.rendererBackgroundPhaseScore = this.objectBackgroundPhaseScore.GetComponent<SpriteRenderer>();
        this.rendererBackgroundTotalScore = this.objectBackgroundTotalScore.GetComponent<SpriteRenderer>();
        this.rendererBackgroundTutorial = this.objectBackgroundTutorial.GetComponent<SpriteRenderer>();

        this.textSlideNumber = this.objectTextSlideNumber.GetComponent<Text>();
    }

    private void Start() {
        UpdateColors();
        this.scriptBGMManager.PlayMusicNewScene();
    }

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
        this.rendererBackgroundTutorial.color = this.scriptGameOption.ColorBackground;
    }

    private void UpdateColorOfCanvasText() {
        if (this.scriptGameOption.OptionColorType == GameOption.ColorType.Original) {
            this.textSlideNumber.color = this.scriptGameOption.ColorTextSub;

        } else {
            this.textSlideNumber.color = this.scriptGameOption.ColorTextMain;
        }
    }

    private void UpdateColorOfCanvasButton() {
        if (this.scriptGameOption.OptionColorType == GameOption.ColorType.Original) {
            this.imageTitle.color = this.scriptGameOption.ColorAccent;
            this.imageTitleIcon.color = this.scriptGameOption.ColorTextMain;

        } else {
            this.imageTitle.color = this.scriptGameOption.ColorTextMain;
            this.imageTitleIcon.color = this.scriptGameOption.ColorAccent;
        }

        this.imageNext.color = this.scriptGameOption.ColorPanelB;
        this.imageNextIcon.color = this.scriptGameOption.ColorTextMain;

        this.imagePrevious.color = this.scriptGameOption.ColorPanelA;
        this.imagePreviousIcon.color = this.scriptGameOption.ColorTextMain;
    }
}
