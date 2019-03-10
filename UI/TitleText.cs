using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class TitleText : MonoBehaviour {
    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectOptionManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameOption scriptGameOption;

    private void Awake() {
        this.objectOptionManager = GameObject.FindWithTag("OptionManager");
        this.scriptGameOption = this.objectOptionManager.GetComponent<GameOption>();
    }

    private void Start() {
        UpdateText();
    }

    private void UpdateText() {
        foreach (Transform transformText in this.transform) {
            if (transformText.name == "TextVersion") {
                UpdateTextVersion(transformText.GetComponent<Text>());
            }

            if (transformText.name == "TextDebug") {
                UpdateTextDebug(transformText.gameObject);
            }
        }
    }

    private void UpdateTextVersion(Text textVersion) {
        textVersion.text = this.scriptGameOption.GameVersion;
    }

    // デバッグ用（デバッグモードの解除し忘れ防止のため）
    private void UpdateTextDebug(GameObject objectTextDebug) {
        if (this.scriptGameOption.DebugMode) {
            objectTextDebug.SetActive(true);
        } else {
            objectTextDebug.SetActive(false);
        }
    }
}
