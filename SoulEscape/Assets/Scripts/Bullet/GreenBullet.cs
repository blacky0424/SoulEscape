using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 緑弾を管理するクラス
/// </summary>
public class GreenBullet : Bullet
{
    //上下に移動する幅
    float height;
    //左右に移動する幅
    float width;
    float timer;

    private void OnEnable()
    {
        height = Random.Range(1, 5);
        width = Random.Range(1, 5);
        timer = 0;
    }

    protected override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        if(timer > 1.0f)
        {
            timer = 0;
            height = Random.Range(1, 5);
            width = Random.Range(1, 5);
        }
        Vector3 movePos = new Vector3(direction.x + Mathf.Sin(Time.time * width), Mathf.Cos(Time.time * height));
        //移動
        transform.position += movePos * speed;
        //端についたら初期化
        if (transform.position.x < -firstPosX)
        {
            initialize();
        }
    }
}
