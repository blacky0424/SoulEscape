using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// セレクトシーンを管理するクラス
/// </summary>
public class SelectScene : SingletonMonoBehaviour<SelectScene> {

    [SerializeField]
    UnityEngine.UI.Button[] buttonBox;

	void Start () {
        FadeManager.Instance.fadeColor = Color.black;
        FadeManager.Instance.FadeIn();

        for(int i = 0;i < ScoreManager.Instance.ClearNum + 1; i++)
        {
            buttonBox[i].interactable = true;
        }
	}

    public void LoadTitleScene()
    {
        FadeManager.Instance.fadeColor = Color.black;
        SceneManager.Instance.LoadScene(SceneManager.Title_Scene);
    }

    public void LoadStage1()
    {
        ScoreManager.Instance.Stage = 1;
        SceneManager.Instance.LoadScene(SceneManager.Stage1_Scene);
    }

    public void LoadStage2()
    {
        ScoreManager.Instance.Stage = 2;
        SceneManager.Instance.LoadScene(SceneManager.Stage2_Scene);
    }

    public void LoadStage3()
    {
        ScoreManager.Instance.Stage = 3;
        SceneManager.Instance.LoadScene(SceneManager.Stage3_Scene);
    }

    public void Release()
    {
        for (int i = 0; i < buttonBox.Length; i++)
        {
            buttonBox[i].interactable = true;
        }
    }
}
