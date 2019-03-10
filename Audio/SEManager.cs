using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SEManager :MonoBehaviour {
    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private AudioSource audioSource;

    [BoxGroup("GameObject"), ShowInInspector, ReadOnly]
    private GameObject objectOptionManager;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private GameOption scriptGameOption;

    [SerializeField, BoxGroup("Audio")]
    private AudioClip seStart;
    [SerializeField, BoxGroup("Audio")]
    public AudioClip seAlerm;
    [SerializeField, BoxGroup("Audio")]
    public AudioClip seSelect;
    [SerializeField, BoxGroup("Audio")]
    public AudioClip seDropA, seDropB, seDropC;

    private void Awake() {
        this.objectOptionManager = GameObject.FindWithTag("OptionManager");
        this.scriptGameOption = this.objectOptionManager.GetComponent<GameOption>();

        this.audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        UpdateVolume();
    }

    public void UpdateVolume() {
        this.audioSource.volume = this.scriptGameOption.VolumeSE;
    }

    public void PlaySE(AudioClip se) {
        audioSource.PlayOneShot(se);
    }

    public void PlaySEStart() {
        PlaySE(seStart);
    }

    public void PlaySESelect() {
        PlaySE(seSelect);
    }

    public void PlaySEReplace() {
        // SE一旦なし
    }

    public void PlaySEDrop(int count) {
        // count：一度に落とす数（高いほどハイスコア）

        if (count >= 20) {
            PlaySE(seDropC);
        } else if (count >= 10) {
            PlaySE(seDropB);
        } else if (count >= 5) {
            PlaySE(seDropA);
        }
    }

    public void PlaySEAlerm() {
        PlaySE(seAlerm);
    }

    public void PlayRandomSE() {
        // サウンドテスト用

        switch (Random.Range(0, 5 + 1)) {
            case 0:
                PlaySE(this.seStart);
                if (this.scriptGameOption.DebugMode) {
                    Debug.Log("PlayRandomSE START");
                }
                break;

            case 1:
                PlaySE(this.seAlerm);
                if (this.scriptGameOption.DebugMode) {
                    Debug.Log("PlayRandomSE ALERM");
                }
                break;

            case 2:
                PlaySE(this.seDropA);
                if (this.scriptGameOption.DebugMode) {
                    Debug.Log("PlayRandomSE DROP A");
                }
                break;

            case 3:
                PlaySE(this.seDropB);
                if (this.scriptGameOption.DebugMode) {
                    Debug.Log("PlayRandomSE DROP B");
                }
                break;

            case 4:
                PlaySE(this.seDropC);
                if (this.scriptGameOption.DebugMode) {
                    Debug.Log("PlayRandomSE DROP C");
                }
                break;

            case 5:
                PlaySE(this.seSelect);
                if (this.scriptGameOption.DebugMode) {
                    Debug.Log("PlayRandomSE SELECT");
                }
                break;
        }
    }
}