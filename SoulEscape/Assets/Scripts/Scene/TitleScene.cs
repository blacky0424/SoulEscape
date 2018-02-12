using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : SingletonMonoBehaviour<TitleScene> {

    [SerializeField]
    GameObject t;

    float timer;

    void Start () {
        FadeManager.Instance.fadeColor = Color.black;
        FadeManager.Instance.FadeIn();
    }
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.Instance.LoadScene(SceneManager.Select_Scene);
        }

        if (timer > 1.0f)
        {
            timer = 0;
            if (t.activeSelf)
            {
                t.SetActive(false);
            }
            else
            {
                t.SetActive(true);
            }
        }

        timer += Time.deltaTime;
    }

}
