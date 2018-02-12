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
        ScoreManager.Instance.ResetScore();
        FadeManager.Instance.fadeColor = Color.black;
        SceneManager.Instance.LoadScene(SceneManager.Title_Scene);
    }

    public void LoadSelectScene()
    {
        ScoreManager.Instance.ResetScore();
        FadeManager.Instance.fadeColor = Color.black;
        SceneManager.Instance.LoadScene(SceneManager.Select_Scene);
    }
}
