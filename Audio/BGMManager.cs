using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class BGMManager :MonoBehaviour {
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private AudioSource audioSource;

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectOptionManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameOption scriptGameOption;

    [SerializeField, BoxGroup("Audio")]
    private AudioClip bgmGameTitle;
    [SerializeField, BoxGroup("Audio")]
    private AudioClip bgmResult;
    [SerializeField, BoxGroup("Audio")]
    private AudioClip bgmGameFirstA;
    [SerializeField, BoxGroup("Audio")]
    private AudioClip bgmGameFirstB;
    [SerializeField, BoxGroup("Audio")]
    private AudioClip bgmGameLatter;

    private void Awake() {
        this.objectOptionManager = GameObject.FindWithTag("OptionManager");
        this.scriptGameOption = this.objectOptionManager.GetComponent<GameOption>();

        this.audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        UpdateVolume();
    }

    public void UpdateVolume() {
        this.audioSource.volume = this.scriptGameOption.VolumeBGM;
    }

    private void PlayMusic() {
        this.audioSource.Play();
    }

    public void StopMusic() {
        this.audioSource.Stop();
    }

    private void ChangeMusic(AudioClip newMusic) {
        if (this.audioSource.clip != newMusic) { // 同じBGMの場合に最初から再生しなおさないように
            StopMusic();
            this.audioSource.clip = newMusic;
            PlayMusic();
        }
    }

    private void ChangeMusicInGame(AudioClip newMusic) {
        StopMusic();
        this.audioSource.clip = newMusic;
        PlayMusic();
    }


    public void PlayMusicNewScene() {
        // シーン開始時に呼ばれる

        this.audioSource.loop = true;

        switch (SceneManager.GetActiveScene().name) {
            case "Title":
            case "Tutorial":
            case "Option":
                ChangeMusic(this.bgmGameTitle);
                break;

            case "Game":
                if (Random.Range(0, 2) == 0) {
                    ChangeMusicInGame(this.bgmGameFirstA);
                } else {
                    ChangeMusicInGame(this.bgmGameFirstB);
                }
                break;
        }
    }

    public void SwitchGameMusic() {
        // 6フェーズ目でBGMを変更

        ChangeMusic(this.bgmGameLatter);
    }

    public void SwitchMusic() {
        // サウンドテスト用

        if (this.audioSource.clip == this.bgmGameTitle) {  // AudioClipでswitch文使えないみたい
            ChangeMusic(this.bgmGameFirstA);

        } else if (this.audioSource.clip == this.bgmGameFirstA) {
            ChangeMusic(this.bgmGameFirstB);

        } else if (this.audioSource.clip == this.bgmGameFirstB) {
            ChangeMusic(this.bgmGameLatter);

        } else if (this.audioSource.clip == this.bgmGameLatter) {
            ChangeMusic(this.bgmResult);

        } else if (this.audioSource.clip == this.bgmResult) {
            ChangeMusic(this.bgmGameTitle);

        }

    }

    public void PlayResultMusic() {
        ChangeMusic(this.bgmResult);
    }
}