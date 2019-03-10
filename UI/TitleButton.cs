using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class TitleButton : MonoBehaviour {
    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectTitleDirector;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private TitleDirector scriptTitleDirector;

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectBGMManager, objectSEManager;

    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private SEManager scriptSEManager;

    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private BGMManager scriptBGMManager;

    private void Awake() {
        this.scriptTitleDirector = this.objectTitleDirector.GetComponent<TitleDirector>();

        this.objectBGMManager = GameObject.FindWithTag("BGMManager");
        this.scriptBGMManager = this.objectBGMManager.GetComponent<BGMManager>();

        this.objectSEManager = GameObject.FindWithTag("SEManager");
        this.scriptSEManager = this.objectSEManager.GetComponent<SEManager>();
    }

    public void ButtonStart() {
        this.scriptBGMManager.StopMusic();
        this.scriptSEManager.PlaySEStart();
        Invoke("GameStart", 1.0f);
    }

    public void ButtonTutorial() {
        SceneManager.LoadScene("Tutorial");
    }

    public void ButtonRanking() {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(0);
    }

    public void ButtonOption() {
        SceneManager.LoadScene("Option");
    }

    private void GameStart() {
        SceneManager.LoadScene("Game");
    }
}
