using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScene : MonoBehaviour {

    AudioSource audio;

    void Start () {
        FadeManager.Instance.fadeColor = Color.black;
        FadeManager.Instance.FadeIn();

        audio = GetComponent<AudioSource>();
    }

    public void LoadTitleScene()
    {
        audio.PlayOneShot(audio.clip);
        ScoreManager.Instance.ResetScore();
        FadeManager.Instance.fadeColor = Color.black;
        SceneManager.Instance.LoadScene(SceneManager.Title_Scene);
    }

    public void LoadSelectScene()
    {
        audio.PlayOneShot(audio.clip);
        ScoreManager.Instance.ResetScore();
        FadeManager.Instance.fadeColor = Color.black;
        SceneManager.Instance.LoadScene(SceneManager.Select_Scene);
    }
}
