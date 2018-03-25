using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SEName
{
    PlayerDamaged,
    Shot,
    DestroyBullet,
    DestroyBoss,
    TapStart,
    PushButton,
}

public class SoundManager : SingletonMonoBehaviour<SoundManager> {

    public static new SoundManager Instance
    {
        get
        {
            if(instance != null)
            {
                return instance;
            }

            instance = FindObjectOfType<SoundManager>();
            return instance;
        }
    }

    [SerializeField]
    List<AudioClip> soundList = new List<AudioClip>();
    AudioSource audiose;

    void Awake()
    {
        audiose = GetComponent<AudioSource>();

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

    public void PlaySE(SEName soundName)
    {
        audiose.PlayOneShot(soundList[(int)soundName]);
    }
}
