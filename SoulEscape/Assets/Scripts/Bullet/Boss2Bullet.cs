using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Bullet : Bullet {

    Transform boss;
    Transform player;

    private void Awake()
    {
        //向きの修正
        transform.Rotate(0.0f, 0.0f,90.0f);
        boss = FindObjectOfType<OldWomanAndMan>().transform;
        player = FindObjectOfType<Player>().transform;
    }

    private void OnEnable()
    {
        firstPosX = 9.0f;
        direction = GameScene.Instance.GetPlayer.transform.position - boss.position;
    }

    protected override void Update()
    {
        base.Update();

        //移動
        transform.position += direction * speed;
        //端についたら初期化
        if (transform.position.x < -firstPosX)
        {
            initialize();
        }
    }
}
