using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameButton :MonoBehaviour {
    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectGameDirector;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameDirector scriptGameDirector;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Score scriptScore;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private VaryPanel scriptVaryPanel;

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectBGMManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private BGMManager scriptBGMManager;

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectSEManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private SEManager scriptSEManager;

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectOptionManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameOption scriptGameOption;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject buttonDrop, buttonReplace, buttonRetryInGame;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Image imageButtonDrop, imageButtonReplace;
    [SerializeField, BoxGroup("GameObject")]
    private GameObject buttonTweet, buttonRanking, buttonRetry;

    private void Awake() {
        this.scriptGameDirector = this.objectGameDirector.GetComponent<GameDirector>();
        this.scriptScore = this.objectGameDirector.GetComponent<Score>();
        this.scriptVaryPanel = this.objectGameDirector.GetComponent<VaryPanel>();

        this.objectOptionManager = GameObject.FindWithTag("OptionManager");
        this.scriptGameOption = this.objectOptionManager.GetComponent<GameOption>();

        this.objectBGMManager = GameObject.FindWithTag("BGMManager");
        this.scriptBGMManager = this.objectBGMManager.GetComponent<BGMManager>();

        this.objectSEManager = GameObject.FindWithTag("SEManager");
        this.scriptSEManager = this.objectSEManager.GetComponent<SEManager>();

        this.imageButtonDrop = this.buttonDrop.GetComponent<Image>();
        this.imageButtonReplace = this.buttonReplace.GetComponent<Image>();
    }

    private void Start() {
        SwitchActiveButtonGame();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.D)) {
            ButtonDrop();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            ButtonReplace();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            ButtonReplace();
        }
    }

    public void ButtonDrop() {
        if (!this.scriptGameDirector.IsRunning) {
            return;
        }

        // 隙間がないと落とせない
        if (!(this.scriptGameDirector.HasSelected)) {
            return;
        }

        // お手つき
        if (this.scriptGameDirector.TimerPenalty > 0) {
            return;
        }

        this.scriptVaryPanel.DropPanels();
        this.scriptVaryPanel.FillPanels();
    }

    public void ButtonReplace() {
        if (!this.scriptGameDirector.IsRunning) {
            return;
        }

        // 消えているパネルがあるとき入れ替えられない
        if (this.scriptGameDirector.HasSelected) {
            return;
        }

        // お手つき
        if (this.scriptGameDirector.TimerPenalty > 0) {
            return;
        }

        this.scriptGameDirector.ResetPanels();
        this.scriptSEManager.PlaySEReplace();
    }

    public void ButtonRetry() {
        this.scriptGameDirector.IsRunning = false;
        this.scriptBGMManager.StopMusic();

        this.scriptSEManager.PlaySEStart();
        Invoke("Retry", 1.0f);
    }

    public void ButtonTitle() {
        LoadTitle();
    }

    private void Retry() {
        SceneManager.LoadScene("Game");
    }

    private void LoadTitle() {
        SceneManager.LoadScene("Title");
    }

    public void ButtonRanking() {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(this.scriptScore.GetTotalScore());
    }

    public void ButtonTweet() {
        int score = this.scriptScore.GetTotalScore();
        string tweetSentence = "【クロスデカゴン " + this.scriptGameOption.GameVersion + "】今回のスコア：" + score + "(" + this.scriptScore.Rank + "）";
        naichilab.UnityRoomTweet.TweetWithImage("cross_decagon", tweetSentence, "unity1week", "cross_decagon");
    }

    public void SwitchActiveButtonGame() {
        buttonDrop.SetActive(true);
        buttonReplace.SetActive(true);
        buttonRetryInGame.SetActive(true);

        buttonTweet.SetActive(false);
        buttonRanking.SetActive(false);
        buttonRetry.SetActive(false);
    }

    public void SwitchActiveButtonResult() {
        buttonDrop.SetActive(false);
        buttonReplace.SetActive(false);
        buttonRetryInGame.SetActive(false);

        buttonTweet.SetActive(true);
        buttonRanking.SetActive(true);
        buttonRetry.SetActive(true);
    }
}
