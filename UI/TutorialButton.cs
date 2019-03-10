using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class TutorialButton : MonoBehaviour {
    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectCanvasSlide;

    [SerializeField, BoxGroup("GameObject")]
    private GameObject objectTextSlideNumber;
    [BoxGroup("Component"), ShowInInspector, ReadOnly]
    private Text textSlideNumber;

    [BoxGroup("Slide"), ShowInInspector, ReadOnly]
    private int slideNumber = 1;

    [BoxGroup("Slide"), ShowInInspector, ReadOnly]
    private const int NumOfSlides = 12;

    private void Awake() {
        this.textSlideNumber = this.objectTextSlideNumber.GetComponent<Text>();
    }

    private void Start() {
        UpdateTextSlideNumber();
    }

    private void UpdateTutorial() {
        foreach (Transform slide in this.objectCanvasSlide.transform) {
            if (slide.name == slideNumber.ToString()) {
                slide.gameObject.SetActive(true);
            } else {
                slide.gameObject.SetActive(false);
            }
        }

        UpdateTextSlideNumber();
    }

    private void UpdateTextSlideNumber() {
        this.textSlideNumber.text = this.slideNumber.ToString() + " / " + NumOfSlides.ToString();
    }

    public void ButtonNext() {
        this.slideNumber++;
        if (this.slideNumber > NumOfSlides) {
            this.slideNumber = 1;
        }
        UpdateTutorial();
    }

    public void ButtonPrevious() {
        this.slideNumber--;
        if (this.slideNumber < 1) {
            this.slideNumber = NumOfSlides;
        }
        UpdateTutorial();
    }

    public void ButtonTitle() {
        SceneManager.LoadScene("Title");
    }
}
