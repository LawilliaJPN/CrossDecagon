using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class OptionSlider :MonoBehaviour {

    // スクリプト

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectOptionManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameOption scriptGameOption;

    // スライダー

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectSliderBGMVolume, objectSliderSEVolume;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Slider sliderBGMVolume, sliderSEVolume;

    // テキスト

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectTextBGMVolume, objectTextSEVolume;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text textBGMVolume, textSEVolume;

    // Awake Start Update

    private void Awake() {
        this.objectOptionManager = GameObject.FindWithTag("OptionManager");
        this.scriptGameOption = this.objectOptionManager.GetComponent<GameOption>();

        this.sliderBGMVolume = this.objectSliderBGMVolume.GetComponent<Slider>();
        this.sliderSEVolume = this.objectSliderSEVolume.GetComponent<Slider>();

        this.textBGMVolume = this.objectTextBGMVolume.GetComponent<Text>();
        this.textSEVolume = this.objectTextSEVolume.GetComponent<Text>();
    }

    private void Start() {
        UpdateSliders();
    }

    // スライダー On Value Changed

    public void SliderBGMVolume() {
        this.scriptGameOption.VolumeBGM = this.sliderBGMVolume.value / 10;
        this.textBGMVolume.text = "BGM音量:" + (this.scriptGameOption.VolumeBGM * 100).ToString();
    }

    public void SliderSEVolume() {
        this.scriptGameOption.VolumeSE = this.sliderSEVolume.value / 10;
        this.textSEVolume.text = "SE音量:" + (this.scriptGameOption.VolumeSE * 100).ToString();
    }

    // スライダー反映

    private void UpdateSliders() {
        this.sliderBGMVolume.value = this.scriptGameOption.VolumeBGM * 10;
        this.sliderSEVolume.value = this.scriptGameOption.VolumeSE * 10;
        this.textBGMVolume.text = "BGM音量:" + (this.scriptGameOption.VolumeBGM * 100).ToString();
        this.textSEVolume.text = "SE音量:" + (this.scriptGameOption.VolumeSE * 100).ToString();
    }
}
