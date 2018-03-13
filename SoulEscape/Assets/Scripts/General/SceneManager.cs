using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーン遷移を管理するクラス
/// </summary>
public class SceneManager : SingletonMonoBehaviour<SceneManager>
{

    public static new SceneManager Instance
    {
        
        get
        {
            if (instance != null)
            {
                return instance;
            }

            instance = FindObjectOfType<SceneManager>();

            if (instance == null)
            {
                GameObject obj = new GameObject(typeof(SceneManager).Name);
                instance = obj.AddComponent<SceneManager>();
            }

            return instance;
        }
    }

    protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    //呼び出すシーンの名前
    public const string Title_Scene = "Title";
    public const string Select_Scene = "Select";
    public const string Stage1_Scene = "Stage1";
    public const string Stage2_Scene = "Stage2";
    public const string Stage3_Scene = "Stage3";
    public const string Result_Scene = "Result";
    public const string GameOver_Scene = "GameOver";
    public const string GameClear_Scene = "GameClear";

    const string Loading = "IsLoading";

    /// <summary>
    /// 任意のシーンを呼び出す
    /// </summary>
    /// <param name="scene"></param>
    public void LoadScene(string scene)
    {
        //フェード終了時のみ遷移可能
        if (!FadeManager.Instance.fadeinSwitch)
        {
            FadeManager.Instance.FadeOut();
            StartCoroutine(Loading, scene);
        }
    }

    /// <summary>
    ///　シーンを呼び出すまでのフェードの時間確保
    /// </summary>
    /// <param name="scene"></param>
    /// <returns></returns>
    IEnumerator IsLoading(string scene)
    {
        yield return new WaitForSeconds(2.0f);

        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}
