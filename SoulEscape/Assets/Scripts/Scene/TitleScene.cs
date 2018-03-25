using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : SingletonMonoBehaviour<TitleScene> {

    [SerializeField]
    GameObject startText;
    float blinkTimer;
    //タップされたかどうか
    bool isTap;

    void Start () {
        FadeManager.Instance.fadeColor = Color.black;
        FadeManager.Instance.FadeIn();

        isTap = false;
    }
	
	void Update () {
        if (Input.GetMouseButtonDown(0) && !FadeManager.Instance.IsFade() && !isTap)
        {
            isTap = true;
            SoundManager.Instance.PlaySE(SEName.TapStart);
            SceneManager.Instance.LoadScene(SceneManager.Select_Scene);
        }

        //TapToStartテキストの点滅
        if (blinkTimer > 1.0f)
        {
            blinkTimer = 0;
            if (startText.activeSelf)
            {
                startText.SetActive(false);
            }
            else
            {
                startText.SetActive(true);
            }
        }

        blinkTimer += Time.deltaTime;
    }
}
