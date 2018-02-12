using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonMonoBehaviour<ScoreManager> {

    public static new ScoreManager Instance
    {
        get
        {
            if(instance != null)
            {
                return instance;
            }

            instance = FindObjectOfType<ScoreManager>();

            if(instance == null)
            {
                GameObject obj = new GameObject(typeof(ScoreManager).Name);
                instance = obj.AddComponent<ScoreManager>();
            }

            return instance;
        }
    }

    //スコア
    int score;
    //選択ステージ
    int stage;
    //クリアした数
    int clearNum = 0;


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

    public int Score {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    public int Stage
    {
        get
        {
            return stage;
        }
        set
        {
            stage = value;
        }
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int ClearNum
    {
        get
        {
            return clearNum;
        }
        set
        {
            clearNum = value;
        }
    }

}
