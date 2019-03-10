using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class PreloadDirector : MonoBehaviour {
    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectAudio;
    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectOptionManager;

    private void Start() {
        DontDestroyOnLoad(this.objectAudio);
        DontDestroyOnLoad(this.objectOptionManager);

        SceneManager.LoadScene("Title");
    }
}
