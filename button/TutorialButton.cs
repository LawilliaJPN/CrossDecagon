using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// 「遊び方」のボタン関連
public class TutorialButton : MonoBehaviour {
    // 何枚目を表示しているか
    int slideNumber = 1;

    // 全部で何枚のスライドがあるか（定数）
    int NumOfSlides = 12;

    /// 画面に表示するスライドを更新する
    void UpdateTutorial() {
        GameObject slides = GameObject.Find("CanvasSlide");

        foreach (Transform slide in slides.transform) {
            if (slide.name == slideNumber.ToString()) {
                slide.gameObject.SetActive(true);
            } else {
                slide.gameObject.SetActive(false);
            }
        }
    }

    /// 「→」ボタンを押したとき
    public void ButtonNext() {
        slideNumber++;

        // ループ
        if (slideNumber > NumOfSlides) {
            slideNumber = 1;
        }

        UpdateTutorial();
    }

    /// 「←」ボタンを押したとき
    public void ButtonPrevious()
    {
        slideNumber--;

        // ループ
        if (slideNumber < 1) {
            slideNumber = NumOfSlides;
        }

        UpdateTutorial();
    }

    /// 「×」ボタンを押したとき
    public void ButtonTitle() {
        SceneManager.LoadScene("Title");
    }
}