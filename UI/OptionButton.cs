using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class OptionButton : MonoBehaviour {

    // スクリプト

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectOptionDirector;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private OptionDirector scriptOptionDirector;

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectBGMManager, objectSEManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private SEManager scriptSEManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private BGMManager scriptBGMManager;

    // Awake Start Update

    private void Awake() {
        this.scriptOptionDirector = this.objectOptionDirector.GetComponent<OptionDirector>();

        this.objectBGMManager = GameObject.FindWithTag("BGMManager");
        this.scriptBGMManager = this.objectBGMManager.GetComponent<BGMManager>();

        this.objectSEManager = GameObject.FindWithTag("SEManager");
        this.scriptSEManager = this.objectSEManager.GetComponent<SEManager>();
    }

    // ボタン On Click

    public void ButtonMenuTheme() {
        this.scriptOptionDirector.UpdateOptionMenu(OptionDirector.OptionMenu.Theme);
    }

    public void ButtonMenuSound() {
        this.scriptOptionDirector.UpdateOptionMenu(OptionDirector.OptionMenu.Sound);
    }

    public void ButtonSwitchTheme() {
        this.scriptOptionDirector.SwitchTheme();
    }

    public void ButtonTitle() {
        SceneManager.LoadScene("Title");
    }

    public void ButtonTestBGM() {
        this.scriptBGMManager.SwitchMusic();
    }

    public void ButtonTestSE() {
        this.scriptSEManager.PlayRandomSE();
    }
}
