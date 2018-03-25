using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フェードを管理するクラス
/// </summary>
public class FadeManager : SingletonMonoBehaviour<FadeManager> {

    [SerializeField]
    float fadeSpeed;
    //フェードする色
    public Color fadeColor = Color.black;
    public float alpha = 1.0f;
    [SerializeField]
    Image fadeImage;

    public bool fadeinSwitch;
    public bool fadeoutSwitch;

    bool alert;
    int alertCount;
    int bossFade;

    void Awake()
    {
        fadeImage.color = fadeColor;
        fadeinSwitch = false;
        fadeoutSwitch = false;
        alert = false;
        bossFade = 0;
        alertCount = 0;
    }

    
    void Update()
    {

        if (fadeinSwitch)
        {
            alpha -= fadeSpeed;
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);
        }else if (fadeoutSwitch)
        {
            alpha += fadeSpeed;
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);
        }

        if (alpha > 1.0f)
        {
            fadeoutSwitch = false;
            alpha = 1.0f;
        }else if (alpha < 0)
        {
            fadeinSwitch = false;
            alpha = 0f;
        }

        //ボス出現時
        if (alert)
        {
            //画面が赤くなる
            if(bossFade > 0)
            {
                alpha += fadeSpeed;
            }else if(bossFade < 0)
            {
                alpha -= fadeSpeed;
            }

            if(alpha < 0)
            {
                bossFade = 1;
                alertCount++;
            }else if(alpha > 0.5f)
            {
                bossFade = -1;
            }
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha);

            //回数を超えたら初期化
            if(alertCount > 2)
            {
                alertCount = 0;
                alert = false;
                bossFade = 0;
            }
        }
    }
    
    /// <summary>
    /// ボス出現時の演出
    /// </summary>
    public void BossAlert()
    {
        fadeColor = Color.red;
        alert = true;
        bossFade = 1;
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    public void FadeOut()
    {
        fadeoutSwitch = true;
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    public void FadeIn()
    {
        fadeinSwitch = true;
    }

    //フェード中であるかどうか確認
    public bool IsFade()
    {
        if (fadeinSwitch || fadeoutSwitch)
        {
            return true;
        }

        return false;
    }
}
