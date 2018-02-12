using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 青弾を管理するクラス
/// </summary>
public class BlueBullet : Bullet
{
	
	protected override void Update () {
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
