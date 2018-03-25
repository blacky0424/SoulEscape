using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScene : MonoBehaviour {

    void Start () {
        FadeManager.Instance.fadeColor = Color.black;
        FadeManager.Instance.FadeIn();
    }

    public void LoadTitleScene()
    {
        if (!FadeManager.Instance.IsFade())
        {
            SoundManager.Instance.PlaySE(SEName.PushButton);
            ScoreManager.Instance.ResetScore();
            FadeManager.Instance.fadeColor = Color.black;
            SceneManager.Instance.LoadScene(SceneManager.Title_Scene);
        }
    }

    public void LoadSelectScene()
    {
        if (!FadeManager.Instance.IsFade())
        {
            SoundManager.Instance.PlaySE(SEName.PushButton);
            ScoreManager.Instance.ResetScore();
            FadeManager.Instance.fadeColor = Color.black;
            SceneManager.Instance.LoadScene(SceneManager.Select_Scene);
        }
    }
}
