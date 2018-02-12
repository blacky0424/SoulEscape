using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 紫弾を管理するクラス
/// </summary>
public class PurpleBullet : Bullet
{
    //上下に移動する幅
    int height;
    //上下の動きが変化する速さ
    float moveChangeSpeed;
    float timer;

    private void OnEnable()
    { 
        height = Random.Range(1, 6);
        moveChangeSpeed = Random.Range(0, 10.0f);
    }

    protected override void Update()
    {
        base.Update();
        Vector3 movePos = new Vector3(direction.x, Mathf.Cos(Time.time * moveChangeSpeed * height));
        //移動
        transform.position += movePos * speed;
        //端についたら初期化
        if (transform.position.x < -firstPosX)
        {
            initialize();
        }
    }
}
