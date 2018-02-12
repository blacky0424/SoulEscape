using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// プレイヤーの弾を管理するクラス
/// </summary>
public class MyBullet : Bullet {
    void Start () {
        //向きの修正
        transform.Rotate(0.0f, 0.0f, 90.0f);
    }

    protected override void Update () {
        base.Update();
        //移動
        transform.position += direction * speed;
        //画面外に出たとき
        if (transform.position.x > 9.0f )
        {
            initialize();
        }
    }
}
