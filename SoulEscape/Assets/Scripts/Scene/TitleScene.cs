using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : SingletonMonoBehaviour<TitleScene> {

    [SerializeField]
    GameObject t;
    AudioSource audio;

    float timer;

    void Start () {
        FadeManager.Instance.fadeColor = Color.black;
        FadeManager.Instance.FadeIn();

        audio = GetComponent<AudioSource>();
    }
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            audio.PlayOneShot(audio.clip);
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
