using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GameOption : MonoBehaviour {
    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectBGMManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private BGMManager scriptBGMManager;

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectSEManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private SEManager scriptSEManager;


    [BoxGroup("Info"), ShowInInspector, ReadOnly]
    private static bool debugMode = false;

    public bool DebugMode {
        get {
            return debugMode;
        }
    }

    [BoxGroup("Info"), ShowInInspector, ReadOnly]
    private static string gameVersion = "ver.1.3";

    public string GameVersion {
        get {
            return gameVersion;
        }
    }

    [BoxGroup("Audio"), ShowInInspector, ReadOnly]
    private static float volumeBGM = 0.8f;

    public float VolumeBGM {
        get {
            return volumeBGM;
        }
        set {
            volumeBGM = value;

            if (volumeBGM > 1.0f) {
                volumeBGM = 1.0f;
            } else if (volumeBGM < 0) {
                volumeBGM = 0;
            }

            this.scriptBGMManager.UpdateVolume();
        }
    }

    [BoxGroup("Audio"), ShowInInspector, ReadOnly]
    private static float volumeSE = 0.8f;

    public float VolumeSE {
        get {
            return volumeSE;
        }
        set {
            volumeSE = value;

            if (volumeSE > 1.0f) {
                volumeSE = 1.0f;
            } else if (volumeSE < 0) {
                volumeSE = 0;
            }

            this.scriptSEManager.UpdateVolume();
        }
    }

    [BoxGroup("Color"), ShowInInspector, ReadOnly]
    private static ColorType optionColorType = ColorType.LightCH;

    public enum ColorType {
        Original, LightCD, LightCH
    }

    public ColorType OptionColorType {
        get {
            return optionColorType;
        }
        set {
            optionColorType = value;
            SetColorType();
        }
    }

    [BoxGroup("Color"), ShowInInspector, ReadOnly]
    private static Color colorPanelA, colorPanelB;

    [BoxGroup("Color"), ShowInInspector, ReadOnly]
    private static Color colorCamera, colorBackground, colorAccent;

    [BoxGroup("Color"), ShowInInspector, ReadOnly]
    private static Color colorTextMain, colorTextSub, colorTimerBack;

    public Color ColorPanelA {
        get {
            return colorPanelA;
        }
    }

    public Color ColorPanelB {
        get {
            return colorPanelB;
        }
    }

    public Color ColorCamera {
        get {
            return colorCamera;
        }
    }

    public Color ColorBackground {
        get {
            return colorBackground;
        }
    }

    public Color ColorAccent {
        get {
            return colorAccent;
        }
    }

    public Color ColorTextMain {
        get {
            return colorTextMain;
        }
    }

    public Color ColorTextSub {
        get {
            return colorTextSub;
        }
    }

    public Color ColorTimerBack {
        get {
            return colorTimerBack;
        }
    }

    private void Awake() {
        this.objectBGMManager = GameObject.FindWithTag("BGMManager");
        this.scriptBGMManager = this.objectBGMManager.GetComponent<BGMManager>();

        this.objectSEManager = GameObject.FindWithTag("SEManager");
        this.scriptSEManager = this.objectSEManager.GetComponent<SEManager>();
    }

    private void Start() {
        SetColorType();
    }

    public void SwitchColorType() {
        switch (optionColorType) {
            case ColorType.Original:
                OptionColorType = ColorType.LightCD;
                break;

            case ColorType.LightCD:
                OptionColorType = ColorType.LightCH;
                break;

            case ColorType.LightCH:
                OptionColorType = ColorType.Original;
                break;
        }
    }

    private void SetColorType() {
        switch (optionColorType) {
            case ColorType.Original:
                SetColorOriginal();
                break;

            case ColorType.LightCD:
                SetColorLightCD();
                break;

            case ColorType.LightCH:
                SetColorLightCH();
                break;
        }
    }

    public string GetColorTypeName() {
        string colorTypeName = "";

        switch (this.OptionColorType) {
            case ColorType.Original:
                colorTypeName = "Original";
                break;
            case ColorType.LightCD:
                colorTypeName = "Light-CD";
                break;
            case ColorType.LightCH:
                colorTypeName = "Light-CH";
                break;

        }

        return colorTypeName;
    }

    private void SetColorOriginal() {
        // 緑
        colorPanelA = new Color(123f /255f, 162f /255f, 55f /255f, 1.0f);
        // 赤
        colorPanelB = new Color(244f /255f, 33f /255f, 38f /255f, 1.0f);
        // 黄
        colorAccent = new Color(255f /255f, 161f /255f, 0 /255f, 1.0f);
        // 青
        colorBackground = new Color(13f / 255f, 10f / 255f, 138f / 255f, 1.0f);
        // 黒
        colorCamera = new Color(0.1f, 0.1f, 0.1f, 1.0f);
        // 黒
        colorTextMain = new Color(0.15f, 0.15f, 0.15f, 1.0f);
        // 白
        colorTextSub = new Color(0.85f, 0.85f, 0.85f, 1.0f);
        // 灰
        colorTimerBack = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    }

    private void SetColorLightCD() {
        // 緑
        colorPanelA = new Color(149f / 255f, 225f / 255f, 211f / 255f, 1.0f);
        // 赤
        colorPanelB = new Color(243f / 255f, 129f / 255f, 129f / 255f, 1.0f);
        // 黄
        colorAccent = new Color(252f / 255f, 227f / 255f, 138f / 255f, 1.0f);
        // 黄 薄
        colorBackground = new Color(234f / 255f, 255f / 255f, 208f / 255f, 1.0f);
        // 灰 薄 
        colorCamera = new Color(0.6f, 0.6f, 0.6f, 1.0f);
        // 灰 濃
        colorTextMain = new Color(0.4f, 0.4f, 0.4f, 1.0f);
        // 黄
        colorTextSub = new Color(252f / 255f, 227f / 186f, 138f / 255f, 1.0f);
        // 灰
        colorTimerBack = new Color(0.5f, 0.5f, 0.5f, 1.0f);
    }

    private void SetColorLightCH() {
        // 青
        colorPanelA = new Color(165f / 255f, 222f / 255f, 229f / 255f, 1.0f);
        // 黄
        colorPanelB = new Color(254f / 255f, 253f / 186f, 202f / 255f, 1.0f);
        // 赤 薄
        colorAccent = new Color(255f / 255f, 207f / 255f, 223f / 255f, 1.0f);
        // 灰 薄
        colorBackground = new Color(0.7f, 0.7f, 0.7f, 1.0f);
        // 緑
        colorCamera = new Color(224f / 255f, 249f / 255f, 181f / 255f, 1.0f);
        // 灰
        colorTextMain = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        // 灰 薄
        colorTextSub = new Color(0.9f, 0.9f, 0.9f, 1.0f);
        // 灰
        colorTimerBack = new Color(254f / 255f, 253f / 186f, 202f / 255f, 1.0f);
    }
}
