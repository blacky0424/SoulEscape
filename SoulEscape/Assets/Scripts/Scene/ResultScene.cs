using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScene : SingletonMonoBehaviour<ResultScene>
{

    int score;
    [SerializeField]
    Text scoreText;
    const string scoreString = "Score: ";
    [SerializeField]
    Text placeText;
    int stage;
    string place;
    const string place1 = "無事地獄を抜け出しました";
    const string place2 = "無事三途の川を渡り切りました";
    const string place3 = "無事に天国へたどり着きました";


    void Start () {
        FadeManager.Instance.FadeIn();
        score = ScoreManager.Instance.Score;
        stage = ScoreManager.Instance.Stage;
        switch (stage)
        {
            case 1:
                place = place1;
                break;
            case 2:
                place = place2;
                break;
            case 3:
                place = place3;
                break;
        }
    }
	
	void Update () {
        scoreText.text = scoreString + score.ToString();
        placeText.text = place;
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
